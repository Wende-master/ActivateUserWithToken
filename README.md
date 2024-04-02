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
      "Host": "smtp-mail.outlook.com", //El que quieras, puedes decidir el tipo de email
      "Port": 587, //el de outlook
      "EnableSsl": "true",
      "DefaultCredentials": "false"
    }
  }
