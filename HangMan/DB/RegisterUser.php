<?php
include_once 'connection.php';

    $loginUser = $_POST["loginUser"];
    $loginPass = password_hash($_POST["loginPass"], PASSWORD_DEFAULT);
    $loginEmail = $_POST["loginEmail"];

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    session_start();
    $session_id = session_id();

    //Create prepared statement to securely check if the username already exists
    //this makes it impossible to perfom sql injections in our Register screen
    $sql_Username_Check = "SELECT username FROM users WHERE username = ?;";// '" . $loginUser . "'";

    $stmt1 = $conn->prepare($sql_Username_Check);   //prepare the statement
    $stmt1->bind_param("s", $loginUser);            //bind the parameter to the statement as a string
    $stmt1->execute();                              //run the statement

    $result_userName_check = $stmt1->get_result();  //check for results

    //Same thing goes for the email as for the username
    $sql_Email_Check = "SELECT username FROM users WHERE mail = ?;";

    $stmt2 = $conn->prepare($sql_Email_Check);
    $stmt2->bind_param("s", $loginEmail);
    $stmt2->execute();

    $result_email_check = $stmt2->get_result();

    //check if username is available
    if ($result_userName_check->num_rows > 0) {
        $data = array('success' => false, 'error' => "Username is already taken");
    }
    //check if email is available
    else if ($result_email_check->num_rows > 0) {
        $data = array('success' => false, 'error' => "Email is taken");
    }

    //note that password validation is done in unity, using the validation.cs script

    //insert our new user into the database
    else {
        $sql2 = "INSERT INTO users (mail,username,password) VALUES ('" . $loginEmail . "','" . $loginUser . "','" . $loginPass . "' );";
        if ($conn->query($sql2) === TRUE) {
            $data = array('success' => true, 'error' => '', 'username' => "$loginUser", 'session' => $session_id);
            $_SESSION['username'] = $loginUser;
        }
        else {
            $data = array('success' => false, 'error' => $conn->error);
        }
    }
    //echo the data to unity as json
    header('Content-Type: application/json');
    echo json_encode($data);
    $conn->close();

?>


