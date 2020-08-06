<?php
    include_once 'connection.php';

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    $sql = "SELECT username FROM users;";
    $result = $conn->query($sql);
    if ($result->num_rows > 0){
        while($row = $result->fetch_assoc()){
            echo "username: " . $row["username"] . "<br>";
        }
    } else{
        echo "0 results";
    }
    $conn->close();

?>


