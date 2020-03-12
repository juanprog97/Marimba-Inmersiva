int p1;
int p2;
int p3;
String trama;
void setup() {
  Serial.begin(9600);
  
  
}

void loop() {
  trama = "";
  p1 = analogRead(A2); //Sube
  p2 = analogRead(A0);//Baja
  p3 = analogRead(A1); //UNDo
  if(p1 <=600){
    trama+="1";
    delay(20);
  }
  else{
    trama+="0";
    
  }
  if(p2 <=600){
     trama+="1";
     delay(20);
  }
  else{
    trama+="0";
    
  }
  if(p3<=600){
     trama+="1";
     delay(20);
  }
  else{
    trama+="0";
    
  }
  delay(100);
  Serial.println(trama);
  
}
