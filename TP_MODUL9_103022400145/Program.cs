using System;

class Program
{
    static void Main(string[] args)
    {
        CovidConfig konfigurasi = new CovidConfig();

        Console.WriteLine("--- PENGECEKAN PERTAMA ---");
        CekStatusMasuk(konfigurasi);

        Console.WriteLine("\n--- MENGUBAH SATUAN SUHU ---");
        konfigurasi.UbahSatuan();
        Console.WriteLine($"Satuan suhu berhasil diubah menjadi: {konfigurasi.DataConfig.Satuan}");

        Console.WriteLine("\n--- PENGECEKAN KEDUA ---");
        CekStatusMasuk(konfigurasi);

        Console.WriteLine("\nSelesai. Tekan enter untuk menutup...");
        Console.ReadLine();
    }

    static void CekStatusMasuk(CovidConfig konfigurasi)
    {
        Console.WriteLine($"Berapa suhu badan anda saat ini? Dalam nilai {konfigurasi.DataConfig.Satuan}");
        double suhuBadan = double.Parse(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam?");
        int lamaHari = int.Parse(Console.ReadLine());

        bool isSuhuAman = false;

        if (konfigurasi.DataConfig.Satuan == "celcius")
        {
            isSuhuAman = (suhuBadan >= 36.5 && suhuBadan <= 37.5);
        }
        else if (konfigurasi.DataConfig.Satuan == "fahrenheit")
        {
            isSuhuAman = (suhuBadan >= 97.7 && suhuBadan <= 99.5);
        }

        bool isHariAman = lamaHari < konfigurasi.DataConfig.BatasHari;

        if (isSuhuAman && isHariAman)
        {
            Console.WriteLine(konfigurasi.DataConfig.TeksDiterima);
        }
        else
        {
            Console.WriteLine(konfigurasi.DataConfig.TeksDitolak);
        }
    }
}