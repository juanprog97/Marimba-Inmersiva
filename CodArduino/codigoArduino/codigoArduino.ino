void setup() {
  Serial.begin(9600);
  pinMode(4,INPUT_PULLUP);
  pinMode(3,INPUT_PULLUP);
  pinMode(2,INPUT_PULLUP);
  
}

void loop() {
  int pin2 = not(digitalRead(2));
  int pin3 = not(digitalRead(3));
  int pin4 = not(digitalRead(4));
  
  Serial.print(pin2);
  Serial.print(pin3);
  Serial.println(pin4);
  delay(100);
 
}
