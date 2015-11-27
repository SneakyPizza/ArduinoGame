//Entering values that we will use later.
int UD = 0;
int LR = 0;
int UD2 = 0;
int LR2 = 0;

void setup() {
  //The arduino will listen to serial port 9600.
  Serial.begin(9600);
}

void loop() {
  //Assign the UDs and LRs to the right analog pin.
  UD = analogRead(A0);
  LR = analogRead(A1);
  UD2 = analogRead(A2);
  LR2 = analogRead(A3);
  
  //The first 4 way joystick will send values via these serial prints.
  Serial.print(UD, DEC); //The UD being the horizontal axis in DEC(Decimal) numbers.
  Serial.print(","); //We will use this "," for splitting the Horizontal axis and the Vertical axis.
  Serial.print(LR, DEC);   //The LR being the vertical axis in DEC(Decimal) numbers.
  
  //We will use this "," for splitting the 2 joysticks apart inside the unity engine.
  Serial.print(",");
  
 //The second 4 way joystick will send values via these serial prints.
  Serial.print(UD2, DEC);
  Serial.print(",");
  Serial.print(LR2, DEC);
  Serial.println("");
  
  //Use a delay of 200 mili seconds so it won't update to fast.
  delay(200);
}
