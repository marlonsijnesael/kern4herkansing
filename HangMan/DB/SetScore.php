<?php
    include_once 'connection.php';

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    $session_id = $_POST['session_id'];
    $score = $_POST['score'];

    session_id($session_id);
    session_start();

    $user_exists = isset($_SESSION['username']);

    if($user_exists)
    {
        $sql = "INSERT INTO score(username, score) VALUES('" . $_SESSION['username'] . "','$score')";
        $result = mysqli_query($conn ,$sql);
        if ($result)
        {
            $data = array('success' => true, 'error' => '' );
        } else
        {
            $data = array('success' => false, 'error' => 'could not save score' );
        }

        header('Content-Type: application/json');
        echo json_encode($data);
    }
    else
    {
        echo "usnername not set";
    }


?>