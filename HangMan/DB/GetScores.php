<?php
    include_once 'connection.php';

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    //create a simple query to select all the rows in our score table
    $sql = "SELECT * FROM `score`;";


    $data = array();
    $result = $conn->query($sql);

    //check if the result isn't empty -> then loop through our results and put each row into our data array
    if ($result->num_rows > 0) {
        while ($row = $result->fetch_assoc()) {
            $data[] = $row;
        }
    }

    //echo the data to unity as json
    header('Content-Type: application/json');
    echo json_encode($data);
    $conn->close();
?>


