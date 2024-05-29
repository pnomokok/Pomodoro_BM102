#include <Wire.h>
#include "MAX30105.h"
#include "heartRate.h"

MAX30105 particleSensor;

const byte RATE_SIZE = 4; //Ortalama almak için gereken veri sayısı
byte rates[RATE_SIZE]; //Kalp atış hızını depolamak için dizi
byte rateSpot = 0;
long lastBeat = 0; //Son kalp atışı zamanını tutmak için değişken
float beatsPerMinute;
int beatAvg;

void setup() {
  Serial.begin(115200);
  Serial.println("Initializing...");

  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) {
    Serial.println("MAX30102 not found. Please check the wiring.");
    while (1);
  }

  particleSensor.setup(); //Sensörü başlatma
  particleSensor.setPulseAmplitudeRed(0x0A); //RED LED parlaklığı
  particleSensor.setPulseAmplitudeGreen(0); //GREEN LED'i kapatma
}

void loop() {
  long irValue = particleSensor.getIR();

  if (checkForBeat(irValue) == true) {
    long delta = millis() - lastBeat;
    lastBeat = millis();

    beatsPerMinute = 60 / (delta / 1000.0);
    
    if (beatsPerMinute < 255 && beatsPerMinute > 20) {
      rates[rateSpot++] = (byte)beatsPerMinute;
      rateSpot %= RATE_SIZE;

      beatAvg = 0;
      for (byte x = 0 ; x < RATE_SIZE ; x++) beatAvg += rates[x];
      beatAvg /= RATE_SIZE;
    }
  }

  Serial.print("IR=");
  Serial.print(irValue);
  Serial.print(", BPM=");
  Serial.print(beatsPerMinute);
  Serial.print(", Avg BPM=");
  Serial.println(beatAvg);

  if (beatAvg > 100) {
    Serial.println("Yüksek Derecede Stresli");
  } else if (beatAvg > 80) {
    Serial.println("Orta Derecede Stresli");
  } else {
    Serial.println("Düşük Stres Seviyesi");
  }

  delay(1000); // Her saniye bir ölçüm yap
}-
