//#include "deneyap.h"
#include <Wire.h>
#include "MAX30105.h"
#include "heartRate.h"
#include "BluetoothSerial.h"

MAX30105 particleSensor;
BluetoothSerial SerialBT;

const int measurementTime = 10000; // 10 seconds
unsigned long lastBeat = 0;

void setup() {
  Serial.begin(115200);
  Serial.println("Initializing...");
  SerialBT.begin("DeneyapKart");
  Serial.println("Bluetooth initialized");

  // Initialize MAX30102
  Serial.println("Attempting to initialize sensor...");
  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) {
    Serial.println("MAX30102 not found. Please check wiring/power.");
    while (1);
  }

  particleSensor.setup(); // Configure sensor with default settings
  particleSensor.setPulseAmplitudeRed(0x0A); // Turn Red LED to low to indicate sensor is running
  particleSensor.setPulseAmplitudeGreen(0); // Turn off Green LED

  Serial.println("Setup complete");
//  Serial.begin(115200);
//  SerialBT.begin("DeneyapKart");
//
//  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) {
//    Serial.println("MAX30102 not found. Please check wiring/power.");
//    while (1);
//  }
//
//  particleSensor.setup();
//  particleSensor.setPulseAmplitudeRed(0x0A);
//  particleSensor.setPulseAmplitudeGreen(0);
//
//  Serial.println("Setup complete");
}

void loop() {
  if (SerialBT.available()) {
    String command = SerialBT.readString();
    if (command == "start") {
      measureHeartRate();
    }
  }
}

void measureHeartRate() {
  unsigned long startTime = millis();
  long irValue;
  int beatsDetected = 0;
  long sumBeats = 0;

  while (millis() - startTime < measurementTime) {
    irValue = particleSensor.getIR();

    if (checkForBeat(irValue)) {
      float beatsPerMinute = 60 / ((millis() - lastBeat) / 1000.0);
      lastBeat = millis();
      if (beatsPerMinute > 20 && beatsPerMinute < 255) {
        sumBeats += beatsPerMinute;
        beatsDetected++;
      }
    }
    delay(20);
  }

  if (beatsDetected > 0) {
    int averageBPM = sumBeats / beatsDetected;
    SerialBT.println(String(averageBPM));
  } else {
    SerialBT.println("Error");
  }
}

//#include <BluetoothSerial.h>
//#include <esp_bt.h>
//#include <esp_bt_main.h>
//#include <esp_bt_device.h>
//
//BluetoothSerial SerialBT;
//
//void setup() {
//  Serial.begin(115200);
//  if (!SerialBT.begin("DENEYAPKART")) {
//    Serial.println("Bluetooth başlatılamadı");
//  } else {
//    Serial.println("Bluetooth başlatıldı. Şimdi diğer cihazlarla eşleşebilir.");
//    Serial.println("Bluetooth MAC adresi: " + getMacAddress());
//  }
//}
//
//void loop() {
//  Serial.println(getMacAddress());
//}
//
//String getMacAddress() {
//  const uint8_t* mac = esp_bt_dev_get_address();
//  char macStr[18];
//  sprintf(macStr, "%02X:%02X:%02X:%02X:%02X:%02X", mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
//  return String(macStr);
//}
