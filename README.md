# Cumulative Assignments from Web Development Lab Class (HTTP5112)

### Cumulative 1: 
* Build a search interface to find a teacher (by name, hiredate, salary)
* Build the MVP for read functionality on the Teachers table
* Build the MVP for read functionality on the Students table
* Build the MVP for read functionality on the Classes table

### Cumulative 2: 
* To build a minimum viable product (MVP) ‘Add’ as well as ‘Delete’ functionality on the provided teachers mysql table using WebAPI and the MVC architecture pattern
* Use JavaScript and AJAX to send an XHR request for removing a teacher

### Cumulative 3:
#### Initiative:
Show evidence of using a CURL request with a JSON object to update the teacher data through the WebAPI instead of the teacher interface.

[JSON Object](./teacher.json)

```curl -H "Content-Type:application/json" -d @teacher.json "http://localhost:50682/api/TeacherData/UpdateTeacher/5"```
