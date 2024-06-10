#include "deneyap.h" // Deneyap Kart kütüphanesi
#include <WiFi.h> // ESP32 Wi-Fi kütüphanesi
#include <WebServer.h> // ESP32 web sunucu kütüphanesi
#include <ESPmDNS.h> // ESP32 mDNS (Multicast DNS) kütüphanesi
#include "MAX30105.h" // MAX3010x kalp atış hızı sensörü kütüphanesi
#include "heartRate.h" // Kalp atış hızı hesaplamak için kullanılan kütüphane

MAX30105 particleSensor; // MAX30105 sensör nesnesi oluşturuluyor.
WebServer server(80); // 80 numaralı portu kullanarak web sunucusu nesnesi oluşturuluyor.

const char* ssid = "Galaxy A30"; // Bağlanılacak Wi-Fi ağının adı.
const char* password = "isys7294"; // Bağlanılacak Wi-Fi ağının şifresi.
const char* deviceName = "esp32"; // mDNS için cihaz adı belirleniyor.

void setup() {
  Serial.begin(115200); // Seri haberleşme başlatılıyor ve baud hızı 115200 olarak ayarlanıyor.

  // Wi-Fi ağına bağlanma işlemi başlatılıyor.
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) { // Wi-Fi ağına bağlanana kadar bekleniyor.
    delay(1000);
    Serial.println("WiFi'ye bağlanılıyor...");
  }
  Serial.println("WiFi'a bağlandı!");
  Serial.print("IP adresi: ");
  Serial.println(WiFi.localIP()); // Bağlantı sağlandıktan sonra IP adresi seri monitöre yazdırılıyor.

  // mDNS başlatılıyor.
  if (!MDNS.begin(deviceName)) { // mDNS başlatılamazsa hata mesajı seri monitöre yazdırılıyor.
    Serial.println("mDNS başlatılamadı.");
    return;
  }
  Serial.println("mDNS başlatıldı."); // mDNS başarılı bir şekilde başlatıldığında seri monitöre yazdırılıyor.
  Serial.print("mDNS adı: ");
  Serial.print(deviceName);
  Serial.println(".local"); // mDNS adı seri monitöre yazdırılıyor.

  // MAX30102 sensörü başlatılıyor.
  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) { // Sensör bulunamazsa hata mesajı yazdırılıyor ve program durduruluyor.
    Serial.println("MAX30102 sensörü bulunamadı. Lütfen bağlantıları kontrol edin.");
    while (1);
  }

  // Sensör ayarları yapılıyor.
  particleSensor.setup(); // Sensör başlatılıyor.
  particleSensor.setPulseAmplitudeRed(0x0A); // Kırmızı LED'in parlaklığı düşük seviyeye ayarlanıyor.
  particleSensor.setPulseAmplitudeGreen(0); // Yeşil LED kapatılıyor.

  // Web sunucusuna gelen "/start" isteği için handleStart fonksiyonu ayarlanıyor.
  server.on("/start", HTTP_GET, handleStart);
  server.begin(); // Web sunucusu başlatılıyor.
  Serial.println("Web sunucusu başlatıldı.");
}

void loop() {
  server.handleClient(); // Gelen istemci istekleri işleniyor.
}

void handleStart() {
  int measurementTime = 10000; // Ölçüm süresi 10 saniye olarak ayarlanıyor.
  unsigned long startTime = millis(); // Ölçümün başlangıç zamanı kaydediliyor.
  long irValue; // IR sensör değeri için değişken tanımlanıyor. Bu değişken sensörde parmak olup olmadığının anlaşılmasında kullanılıyor.
  int beatsDetected = 0;
  long sumBeats = 0;
  unsigned long lastBeat = 0; // Son nabız zamanı için değişken tanımlanıyor.

  while (millis() - startTime < measurementTime) { // Ölçüm süresi dolana kadar döngüde kalınıyor.
    irValue = particleSensor.getIR(); // IR sensör değeri okunuyor.

    if (checkForBeat(irValue)) { // Nabız tespit edilirse işlem yapılıyor.
      float beatsPerMinute = 60 / ((millis() - lastBeat) / 1000.0); // Nabız hızı hesaplanıyor.
      lastBeat = millis(); // Son nabız zamanı güncelleniyor.
      if (beatsPerMinute > 20 && beatsPerMinute < 255) { // Geçerli bir nabız hızı ise işlem yapılıyor.
        sumBeats += beatsPerMinute;
        beatsDetected++;
      }
    }
    delay(20);
  }

  if (beatsDetected > 0) { // En az bir nabız algılandıysa işlem yapılıyor.
    int averageBPM = sumBeats / beatsDetected; // Ortalama nabız hızı hesaplanıyor.
    server.send(200, "text/plain", String(averageBPM)); // Sonuç web sunucusuna gönderiliyor.
  } else { // Nabız algılanamadıysa hata mesajı gönderiliyor.
    server.send(200, "text/plain", "Hata: Nabız algılanamadı."); // Hata mesajı web sunucusuna gönderiliyor.
  }
}
// Koddaki Serial.print() ve Serial.println() fonksiyonlarının kodun çalışmasına bir katkısı yoktur.
// Herhangi bir sorun oluşması durumunda sorunun hangi kısımda meydana geldiğinin anlaşılmasında yardımcı olması için eklenmişlerdir.
