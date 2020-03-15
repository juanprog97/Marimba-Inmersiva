int p1;
int p2;
int p3;
char trama[3];
String tram;
void setup() {
  Serial.begin(9600);
  
  
}

void loop() {
  tram = "";
  trama[0] = '0';
  trama[1] = '0';
  trama[2] = '0';
  
  p1 = analogRead(A2); //Sube
  p2 = analogRead(A0);//Baja
  p3 = analogRead(A1); //UNDo
  
  
  if(p1 <=350 ){
    trama[0]='1';
    
  }
   if(p2 <=350 ){
    trama[1]='1';
    
  }
   if(p3 <=350 ){
    trama[2]='1';
    
  }
  for(int i = 0; i<3; i++){
    tram+= trama[i];
  }
 
  delay(150);
  Serial.println(tram);
  
}
