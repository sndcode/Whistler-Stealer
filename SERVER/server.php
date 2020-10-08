<?php
$uploads_dir = './logs'; 
if ($_FILES["file"]["error"] == UPLOAD_ERR_OK) 
{
    $tmp_name = $_FILES["file"]["tmp_name"];
    $name = $_FILES["file"]["name"];
    move_uploaded_file($tmp_name, "$uploads_dir/$name".".txt");//Fix for security
}
?>