## TestTaskApi

- api/login takes

  ``` json
  {
  	"password" : "password",
  	"login" : "login"
  }
  ```
	returns token for 200 seconds

  

- api/files/upload takes token in header and file in form

- api/files/download takes token in header and file id in body returns file

- api/myfiles takes token in header returns list of files for that users

  

  