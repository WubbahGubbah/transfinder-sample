Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports System.Windows.Forms

'select school
'select trip

'get points
'if start point, get images
'if counter>1
'take previous coords, next coords
'pass to directions api
'calculate vector between last two point
'pass vector to images api +90,-90
'grab three google images 

Public Class TestApp1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoadTrips.Click

        'load the routes for the selected school

        TextBoxError.Text = ""
        TextBoxDetails.Clear()

        If ListBoxSchools.SelectedIndex = -1 Then
            TextBoxError.Text = "please select a school"
        Else
            Dim thisItem As ListViewItem
            thisItem = ListBoxSchools.SelectedItem

            LoadTrips(CInt(thisItem.ImageKey))
        End If
    End Sub

    Private Function vectorFromStops(inStartLat As Double, inStartLon As Double, inEndLat As Double, inEndLon As Double) As Integer

        'call google maps api for direction steps to have a correct vector for the direction of the bus/road

        'this is the most critial part of this app
        'since you can't use the previous point to the next point, because of street corners, you have to get the path of the street
        'from something like the directions.  So here, we're getting the vector between the two direction points from the google
        'directions api, which should both be on the same street and put the proper vector in the direction the bus is heading

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader

        Try
            ' Create the web request  
            Dim tempURL As String
            tempURL = My.Settings.URLDirections.Replace(My.Settings.URLDirectionsReplaceOriginLatLon, inStartLat.ToString & "," & inStartLon.ToString)
            tempURL = tempURL.Replace(My.Settings.URLDirectionsReplaceDestinationLatLon, inEndLat.ToString & "," & inEndLon.ToString)

            request = DirectCast(WebRequest.Create(tempURL), HttpWebRequest)

            ' Get response  
            response = DirectCast(request.GetResponse(), HttpWebResponse)

            ' Get the response stream into a reader  
            reader = New StreamReader(response.GetResponseStream())

            ' Console application output  
            Dim dirString As String
            dirString = reader.ReadToEnd()

            Dim dirObj As Newtonsoft.Json.Linq.JObject
            dirObj = JsonConvert.DeserializeObject(dirString)

            Dim routesarray As Newtonsoft.Json.Linq.JArray
            routesarray = dirObj.GetValue("routes")

            Dim lastStep As Newtonsoft.Json.Linq.JObject
            Dim lastStepStart As Newtonsoft.Json.Linq.JToken
            Dim lastStepFinish As Newtonsoft.Json.Linq.JToken

            'steps are what we're looking for, specifically, the turn onto the current street segment

            For Each myContent As Newtonsoft.Json.Linq.JObject In routesarray.Children(Of Newtonsoft.Json.Linq.JObject)()
                Dim legsarray As Newtonsoft.Json.Linq.JArray
                legsarray = myContent.SelectToken("legs")
                For Each thisContent As Newtonsoft.Json.Linq.JObject In legsarray.Children(Of Newtonsoft.Json.Linq.JObject)()
                    Dim stepsarray As Newtonsoft.Json.Linq.JArray
                    stepsarray = thisContent.SelectToken("steps")
                    'points in question are the last item in steps, start_location, end_location
                    lastStep = stepsarray.Last
                Next

            Next

            lastStepStart = lastStep.Item("start_location")
            Dim startLat As String
            Dim startLon As String
            startLat = lastStepStart.Item("lat")
            startLon = lastStepStart.Item("lng")
            lastStepFinish = lastStep.Item("end_location")
            Dim endLat As String
            Dim endLon As String
            endLat = lastStepFinish.Item("lat")
            endLon = lastStepFinish.Item("lng")

            Dim thisVector As Integer
            thisVector = calculateVector(startLat, startLon, endLat, endLon)

            Return thisVector


        Catch ex As Exception
            TextBoxError.Text = ex.ToString
            Return Nothing
        End Try
    End Function


    Private Function calculateVector(inStartLat As Double, inStartLon As Double, inEndLat As Double, inEndLon As Double) As Integer

        'old existing imperfect code, but return the bearing based on two points, freely available code

        Dim MetersPerLat As Double = 110950.58
        Dim MetersPerLon As Double = 111317.1
        Dim RAD As Double = (Math.PI / 180)

        Dim finalBearing As Double = 0.0

        Try

            Dim RadBear As Double

            Dim Lat1Rad As Double = inStartLat * RAD
            Dim lat2Rad As Double = inEndLat * RAD
            Dim lon1rad As Double = inStartLon * RAD
            Dim lon2rad As Double = inEndLon * RAD

            RadBear = Math.Atan2(Math.Sin(lon1rad - lon2rad) * Math.Cos(lat2Rad), Math.Cos(Lat1Rad) * Math.Sin(lat2Rad) - Math.Sin(Lat1Rad) * Math.Cos(lat2Rad) * Math.Cos(lon1rad - lon2rad)) Mod 2 * Math.PI

            finalBearing = RadBear / RAD

            If finalBearing < 0 Then
                finalBearing = finalBearing * -1
            Else
                If finalBearing > 0 Then
                    finalBearing = 360 - finalBearing
                End If
            End If

            Return Math.Round(finalBearing)

        Catch ex As Exception
            TextBoxError.Text = ex.ToString
        End Try

        Return finalBearing
    End Function

    Private Function checkVector(inVector As Integer) As Integer
        'don't run something > 360 or < 0

        If inVector > 360 Then
            Return inVector - 360
        ElseIf inVector < 0 Then
            Return inVector + 360
        Else
            Return inVector
        End If
    End Function

    Private Function getReaderString(inURL As String) As String
        'return the string from the reader url

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim resultString As String = ""

        Dim myAuth As String = My.Settings.myAuthCode

        Dim binaryAuthorization As Byte() = System.Text.Encoding.UTF8.GetBytes(myAuth)
        myAuth = Convert.ToBase64String(binaryAuthorization)
        myAuth = "Basic " + myAuth


        Try
            ' Create the web request  
            request = DirectCast(WebRequest.Create(inURL), HttpWebRequest)
            request.Headers.Add("AUTHORIZATION", myAuth)

            ' Get response  
            response = DirectCast(request.GetResponse(), HttpWebResponse)

            ' Get the response stream into a reader  
            reader = New StreamReader(response.GetResponseStream())


            resultString = reader.ReadToEnd

            Return resultString
        Catch ex As Exception
            TextBoxError.Text = ex.ToString
        Finally
            If Not response Is Nothing Then response.Close()
        End Try

        Return resultString

    End Function

    Private Sub LoadSchools()

        Try
            Dim schoolsString As String
            schoolsString = getReaderString(My.Settings.URLSchoolsList)

            Dim schoolsObject As Newtonsoft.Json.Linq.JObject
            schoolsObject = JsonConvert.DeserializeObject(schoolsString)
            For Each thisSchool As Newtonsoft.Json.Linq.JObject In schoolsObject.Item("Objects")
                ListBoxSchools.Items.Add(New ListViewItem(thisSchool.Item("Name").ToString, thisSchool.Item("ID").ToString))
            Next

        Finally

        End Try
    End Sub

    Private Sub LoadTrips(inSchoolID As Integer)
        Dim tripURL As String
        tripURL = My.Settings.URLTripsList.Replace(My.Settings.schoolIDReplaceString, inSchoolID.ToString)

        Dim tripsString As String
        tripsString = getReaderString(tripURL)

        Dim tripsObject As Newtonsoft.Json.Linq.JObject
        tripsObject = JsonConvert.DeserializeObject(tripsString)
        For Each thisTrip As Newtonsoft.Json.Linq.JObject In tripsObject.Item("Objects")
            ListBoxTrips.Items.Add(New ListViewItem(thisTrip.Item("Name").ToString, thisTrip.Item("TripID").ToString))
        Next


    End Sub

    Private Sub LoadStops(inTripID As Integer)

        Dim stopURL As String
        stopURL = My.Settings.URLStopsList.Replace(My.Settings.tripIDReplaceString, inTripID.ToString)

        Dim stopsString As String
        stopsString = getReaderString(stopURL)

        Dim stopsObject As Newtonsoft.Json.Linq.JObject
        stopsObject = JsonConvert.DeserializeObject(stopsString)

        For Each thisStop As Newtonsoft.Json.Linq.JObject In stopsObject.Item("Objects")
            ListBoxStops.Items.Add(New ListViewItem(thisStop.Item("ID").ToString, thisStop.Item("Sequence").ToString))
        Next

    End Sub

    Private Function LoadOneStop(inStopID As Integer) As stopObject

        'parameters for this one stop

        Dim retStop As New stopObject

        retStop.stopID = inStopID

        Dim stopURL As String
        stopURL = My.Settings.URLOneStop.Replace(My.Settings.stopIDReplaceString, inStopID.ToString)

        Dim thisStopString As String
        thisStopString = getReaderString(stopURL)

        Dim stopObj As Newtonsoft.Json.Linq.JObject
        stopObj = JsonConvert.DeserializeObject(thisStopString)

        For Each thisStop As Newtonsoft.Json.Linq.JObject In stopObj.Item("Objects")
            'only one here?
            retStop.stopLat = thisStop.Item("YCoord")
            retStop.stopLon = thisStop.Item("XCoord")
            retStop.stopSequence = thisStop.Item("Sequence")

            TextBoxDetails.Text = thisStop.Item("ID").ToString & vbCrLf & thisStop.Item("XCoord").ToString & vbCrLf & thisStop.Item("YCoord").ToString & vbCrLf & thisStop.Item("DrivingDirections").ToString
        Next


        Return retStop


    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadSchools()
    End Sub


    Private Sub ButtonLoadStops_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLoadStops.Click

        TextBoxError.Text = ""
        TextBoxDetails.Clear()

        If ListBoxTrips.SelectedIndex = -1 Then
            TextBoxError.Text = "please select a trip"
        Else
            Dim thisItem As ListViewItem
            thisItem = ListBoxTrips.SelectedItem

            Loadstops(CInt(thisItem.ImageKey))
        End If

    End Sub

    Private Sub ButtonLoadView_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLoadView.Click

        'this loads the images for this stop from the street view api

        TextBoxError.Text = ""

        If ListBoxStops.SelectedIndex = -1 Then
            TextBoxError.Text = "please select a stop"
        Else
            Dim thisItem As ListViewItem
            thisItem = ListBoxStops.SelectedItem
            Dim thisVector As Integer = 0

            'if you're at the first stop, we don't know the vector to it
            If ListBoxStops.SelectedIndex = 0 Then
                TextBoxError.Text = "unknown previous vector, defaulting north"
                Dim currentStop As New stopObject
                currentStop = LoadOneStop(CInt(thisItem.Text))
                LoadStreetView(currentStop.stopLat, currentStop.stopLon, thisVector, 1)

                Dim leftVector As Integer = thisVector - My.Settings.sideWindowOffset
                Dim rightVector As Integer = thisVector + My.Settings.sideWindowOffset

                leftVector = checkVector(leftVector)
                rightVector = checkVector(rightVector)

                LoadStreetView(currentStop.stopLat, currentStop.stopLon, leftVector, 0)
                LoadStreetView(currentStop.stopLat, currentStop.stopLon, rightVector, 2)
            Else
                'are these loaded in sequence?
                Dim currentStop As New stopObject
                Dim previousStop As New stopObject

                'get current
                currentStop = LoadOneStop(CInt(thisItem.Text))

                'get previous - get previous sequence from list
                Dim prevItem As ListViewItem
                For Each prevItem In ListBoxStops.Items
                    If prevItem.ImageKey = currentStop.stopSequence - 1 Then
                        previousStop = LoadOneStop(CInt(prevItem.Text))
                        Exit For
                    End If
                Next

                If IsNothing(previousStop) Then
                    TextBoxError.Text = "error loading previous stop"
                    Exit Sub
                End If

                'get vector from this and the previous stops
                thisVector = vectorFromStops(previousStop.stopLat, previousStop.stopLon, currentStop.stopLat, currentStop.stopLon)

                'add/subtract 90 and check
                LoadStreetView(currentStop.stopLat, currentStop.stopLon, thisVector, 1)

                Dim leftVector As Integer = thisVector - My.Settings.sideWindowOffset
                Dim rightVector As Integer = thisVector + My.Settings.sideWindowOffset

                leftVector = checkVector(leftVector)
                rightVector = checkVector(rightVector)

                LoadStreetView(currentStop.stopLat, currentStop.stopLon, leftVector, 0)
                LoadStreetView(currentStop.stopLat, currentStop.stopLon, rightVector, 2)


            End If
        End If
    End Sub

    Private Sub LoadStreetView(inLat As Double, inLon As Double, inHeading As Integer, inWindow As Integer)

        'call the google street view api, populate the image windows in question

        Dim tempURL As String
        tempURL = My.Settings.URLStreetView.Replace(My.Settings.URLStreetViewHeadingReplaceString, inHeading.ToString)
        tempURL = tempURL.Replace(My.Settings.URLStreetViewLatReplaceString, inLat.ToString)
        tempURL = tempURL.Replace(My.Settings.URLStreetViewLonReplaceString, inLon.ToString)

        Dim tmpImage As Image = Nothing

        Try

            Dim HttpWebRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(tempURL), System.Net.HttpWebRequest)
            HttpWebRequest.AllowWriteStreamBuffering = True

            Dim WebResponse As System.Net.WebResponse = HttpWebRequest.GetResponse()

            Dim WebStream As System.IO.Stream = WebResponse.GetResponseStream()

            tmpImage = Image.FromStream(WebStream)

            WebResponse.Close()

        Catch ex As Exception

            TextBoxError.Text = ex.ToString

        End Try



        Select Case inWindow
            Case 0
                PictureBoxLeft.Image = tmpImage
            Case 1
                PictureBoxFront.Image = tmpImage
            Case 2
                PictureBoxRight.Image = tmpImage
        End Select
    End Sub

End Class
