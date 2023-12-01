function deleteTeacher() {
    var teacherID = document.getElementById('teacherID').value;

    //Goal is to send a request which looks like this
    // http://localhost:50682/api/TeacherData/DeleteTeacher/2

    var URL = "http://localhost:50682/api/TeacherData/DeleteTeacher/" + teacherID;

    var request = new XMLHttpRequest();

    //where is this request sent to?
    //is the method GET or POST?
    //what should we do with the response?
    request.onreadystatechange = function() {
        //ready state should be 4 AND status should be 200
        if (request.readyState === 4) {
            if (request.status === 200 || request.status === 204) {
                console.log("Teacher with ID " + teacherID + " was deleted successfully")
            } else {
                console.log("Failed to delete teacher with ID " + teacherID)
            }
        }
        
    }

    request.open("POST", URL);
    request.send();
};