# C--ile-Veritabanl--U-ak-Bileti-Sat---Otomasyonu

Projem MsSQL Veritabanı ve C# dili ile oluşturulmuş bir Uçak Bileti Satış Otomasyonudur.

Veritabanı Back-Up dosyası ve proje kaynak kodu içerikte ayrı olarak verilmiştir.

Kullanımın doğru sağlanabilmesi için server bağlantıları değiştirilmeli ve giriş için veritabanında bir kullanıcı kaydı oluşturulmalıdır.

PROJE DETAYI:

Kullanıcıya yönelik bilet satışı, check-In, sefer listeleme, sefer ekleme/silme gibi 7 temel fonksiyonu gerçekleştirebilmekte; yapılan işlemler kullanıcı, işlem ve işlem
saati olarak raporlar halinde tutulmakta ve daha sonrasında raporlara ulaşılabilmektedir. 

Check-In yapılan koltuk üzerinden ikinci bir işlem yapılamamakta ve biletin iptali gibi durumlarda koltuk yeniden aktif olmaktadır.

Direkt uçuşlara ek olarak seferlerin tarihleri, gidiş/dönüş yerleri,kalkış varış saatleri baz alınarak aktarmalı seferler de mevcut hale getirilmiştir.

Aktarmalı, direkt, tek yön/gidiş-dönüş seçenekleri ve yaşlara uygun sefer fiyatı üzerinden fiyatlandırılma ayarlanmıştır.

Her yolcu için özel bir PNR oluşturulmakta ve bu PNR üzerinden güncelleme, check-In, bilet silme gibi işlemler gerçekleştirilmektedir.
