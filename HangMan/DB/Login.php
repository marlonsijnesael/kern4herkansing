<?php
    include_once 'connection.php';

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    //store the username and password from super global $_POST
    $loginUser = $_POST["loginUser"];
    $loginPass = $_POST["loginPass"];

    //Start session
    session_start();
    $session_id = session_id();

    //Create prepared statement to securely check if the username already exists
    //this makes it impossible to perfom sql injections in our login screen
    $sql = "SELECT password FROM users WHERE username = ?;";

    //prepare prepared statement
    $stmt = $conn->prepare($sql);

    //bind parameters to statement, $loginUser will replace the ? with our variable;
    $stmt->bind_param("s", $loginUser);

    //execute statement and store result
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        while ($row = $result->fetch_assoc()) {

            //check if the password matches.
            //Password is encrypted so we use password_verify in order to compare our password on the databse with the password submitted by the user
            if (password_verify($loginPass,$row["password"])){
                $data = array('success' => true, 'error' => '', 'username' => "$loginUser", 'session' => $session_id);
                $_SESSION['username'] = $loginUser;
            }
            else{
                    $data = array('success' => false, 'error' => "Invalid credentials");
            }
        }
    }
    else {
        $data = array('success' => false, 'error' => "User does not exist");
    }

    //echo the data to unity as json
    header('Content-Type: application/json');
    echo json_encode($data);
    $conn->close();

?>


