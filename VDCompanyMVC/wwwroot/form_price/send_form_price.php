<?
function send($token,$text,$chatid){
	$botToken = $token;
	$chats = array($chatid); //269455153
	$url = "https://api.telegram.org/bot".$botToken."/sendMessage";
	
	foreach ($chats as $chat) {
		$params = array(
			'chat_id'=> $chat,
			'text' => $text
		);
		 
		$fields = http_build_query($params, '', '&');
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url."?".$fields ); 
		curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
		$response = curl_exec($ch);
		$statusCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
		if ($statusCode!=200) {
		    //сообщить об ошибке
		}	
		else {
			//
		}
		curl_close($ch);
	}
}	
$input = file_get_contents('php://input');
$input = json_decode($input);

send("1086986092:AAGmnADdJgcABFbbVUnblfqCo-E0Byrqpuw", $input->text, "-1001224074923");
print "Спасибо за заявку! Мы в скором времени с вами свяжемся!";
