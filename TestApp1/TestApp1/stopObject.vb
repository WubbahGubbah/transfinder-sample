
Public Class stopObject
    Private m_stopID As Integer
    Private m_stopLat As Double = 0
    Private m_stopLon As Double = 0
    Private m_stopSequence As Integer

    Public Property stopID As Integer
        Get
            stopID = m_stopID
        End Get
        Set(value As Integer)
            m_stopID = value
        End Set
    End Property

    Public Property stopLat As Double
        Get
            stopLat = m_stopLat
        End Get
        Set(value As Double)
            m_stopLat = value
        End Set
    End Property

    Public Property stopLon As Double
        Get
            stopLon = m_stopLon
        End Get
        Set(value As Double)
            m_stopLon = value
        End Set
    End Property

    Public Property stopSequence As Integer
        Get
            stopSequence = m_stopSequence
        End Get
        Set(value As Integer)
            m_stopSequence = value
        End Set
    End Property
End Class
