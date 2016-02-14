<?php
  $servername = "localhost";
	$username = "root";
	$password = "";
	$dbName = "tom_run";
	;
	$conn = new mysqli($servername, $username, $password, $dbName);
	
	if (!$conn){
	    die("Connection failed.". mysqli_connect_error());
	}
	$sql = "SELECT ID, Health, Damage, Armor, Critical, Speed, Total, Level, Wins FROM runners";
	$result = mysqli_query($conn, $sql);
	
	if(mysqli_num_rows($result)>0){
		while($row = mysqli_fetch_assoc($result)){
			echo "ID:" .$row['ID'] . "|Health:" .$row['Health'] . "|Damage:" .$row['Damage'] ."|Armor:" .$row['Armor'] ."|Critical:" .$row['Critical'] ."|Speed:" .$row['Speed'] ."|Total:" .$row['Total'] ."|Level:" .$row['Level'] ."|Wins:" .$row['Wins'] .(";");
		}
		
	}
?>
