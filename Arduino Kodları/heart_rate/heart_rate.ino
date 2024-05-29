#include "deneyap.h"
#include "BluetoothSerial.h"
#include <Wire.h>
#include "MAX30105.h"
#include "heartRate.h"

BluetoothSerial SerialBT;
MAX30105 particleSensor;

const int measurementDuration = 10; // ölçüm süresi (saniye)
const int measurementInterval = 1000; // ölçüm aralığı (ms)

// Kalp atışı aralığını hesaplamak için kullanılacak değişkenler
long lastBeat = 0;
float beatsPerMinute;
float beatAvg;

void setup() {
    Serial.begin(115200);
    SerialBT.begin("DENEYAPKART"); // Bluetooth ismini ayarlıyoruz
    Wire.begin();

    if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) {
        Serial.println("MAX30105 was not found. Please check wiring/power.");
        while (1);
    }

    particleSensor.setup();
}

void loop() {
    if (SerialBT.available()) {
        char command = SerialBT.read();
        if (command == 'S') {
            startMeasurement();
        }
    }
}

void startMeasurement() {
    float sumBpm = 0;
    int countBpm = 0;

    long startTime = millis();

    while (millis() - startTime < measurementDuration * 1000) {
        long irValue = particleSensor.getIR();
        if (checkForBeat(irValue)) {
            long delta = millis() - lastBeat;
            lastBeat = millis();

            beatsPerMinute = 60.0 / (delta / 1000.0);
            if (beatsPerMinute < 255 && beatsPerMinute > 20) {
                sumBpm += beatsPerMinute;
                countBpm++;
            }
        }
        delay(measurementInterval);
    }

    if (countBpm > 0) {
        float averageBpm = sumBpm / countBpm;
        SerialBT.print(averageBpm);
        SerialBT.print("\n");
    } else {
        SerialBT.print("No beats detected\n");
    }
}
