int p;
void setup() {
  Serial.begin(9600);
  
  
}

void loop() {
  p = analogRead(A0);
  Serial.println(p);
  delay(50);
}
