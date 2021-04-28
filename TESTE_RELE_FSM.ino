// Variáveis de definição das GPIOs
int outSmell = 7;
int outFan  = 8;

// Varíavel utilizada na transmissão serial
int countSerial = 0;

void setup() {
  // Código de configuração do Arduino 
  
  pinMode(outSmell,OUTPUT);
  pinMode(outFan,OUTPUT);

  // Define o baudrate da comunicação serial no Arduino
  Serial.begin(9600);
  
}

// Função que realiza a leitura dos dados recebidos pela porta serial
String readStringSerial(){
  String content = "";
  char caracter;
  
  // Enquanto receber algo pela serial
  while(Serial.available()) {
    // Lê byte da serial
    caracter = Serial.read();
    // Ignora caractere de quebra de linha
    if (caracter != '\n'){
      // Concatena valores
      content.concat(caracter);
    }
    // Aguarda buffer serial ler próximo caractere
    delay(1);
  }    
  return content;
}

void loop(){
  // Se houver dados disponíveis na porta serial executa os comandos 
  if (Serial.available() > 0){

     String ser;
     String output;
     String receivedBuffer = readStringSerial();
     String ptl = receivedBuffer.substring(0,3);
     int key;
     
     // Interpreta o cabeçalho do pacote de dados e define se eles são da "Entrada Digital", "Saída Digital", "LCD" ou "Serial" 
     if(ptl == "OUT")
        key = 1;
     else
        key = -1;

     // De acordo com cabeçalho recebido se manipula a carga útil do pacote de dados
     switch(key){
       case 1:
           if(receivedBuffer.substring(3,5) == "00"){
              digitalWrite(outSmell,LOW);
           }
           if(receivedBuffer.substring(3,5) == "01"){
              digitalWrite(outSmell,HIGH);
           }
           if(receivedBuffer.substring(3,5) == "10"){
              digitalWrite(outFan,LOW);
          }
           if(receivedBuffer.substring(3,5) == "11"){
              digitalWrite(outFan,HIGH);
          }
        
           break;
          
        default:
          break;
     }   
  }    
}
