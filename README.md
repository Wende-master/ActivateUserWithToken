Connection Sql, Crea tu database con lo que quieras y añade la conexión en settings:
"ConnectionStrings": {
  "MiConexion": "UrlDeMiConexion"
}

Mail Settings, utiliza el correo que crees para realizar las pruebas de activación del mismo:
  "MailSettings": {
    "Credentials": {
      "User": "usatucorreopersonalizado@outlook.com", 
      "Password": "tupassword"
    },
    "ServerSmtp": {
      "Host": "smtp-mail.outlook.com",
      "Port": 587,
      "EnableSsl": "true",
      "DefaultCredentials": "false"
    }
  }
