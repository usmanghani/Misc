Public Class ItemData
    Private strval As String
    Private intval As Integer
    'Private strtest As String

    'Public Sub New()
    'End Sub

    'Public Sub New(ByVal strtemp As String, ByVal inttemp As Integer)
    '    strval = strtemp
    '    intval = inttemp
    'End Sub

    Property Value() As String
        Get
            Return strval
        End Get
        Set(ByVal Value As String)
            strval = Value
        End Set
    End Property

    Property ID() As Integer
        Get
            Return intval
        End Get
        Set(ByVal Value As Integer)
            intval = Value
        End Set
    End Property

    'Property test() As String
    '    Get
    '        Return strtest
    '    End Get
    '    Set(ByVal Value As String)
    '        strtest = Value
    '    End Set
    'End Property


    Public Overrides Function ToString() As String
        Return strval 'strtest
    End Function

End Class
