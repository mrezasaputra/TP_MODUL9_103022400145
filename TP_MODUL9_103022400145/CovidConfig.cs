using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PengaturanCovid
{
    [JsonPropertyName("satuan_suhu")]
    public string Satuan { get; set; }

    [JsonPropertyName("batas_hari_demam")]
    public int BatasHari { get; set; }

    [JsonPropertyName("pesan_ditolak")]
    public string TeksDitolak { get; set; }

    [JsonPropertyName("pesan_diterima")]
    public string TeksDiterima { get; set; }

    public PengaturanCovid() { }

    public PengaturanCovid(string satuan, int batasHari, string teksDitolak, string teksDiterima)
    {
        Satuan = satuan;
        BatasHari = batasHari;
        TeksDitolak = teksDitolak;
        TeksDiterima = teksDiterima;
    }
}

public class CovidConfig
{
    public PengaturanCovid DataConfig;
    private const string namaFile = "covid_config.json";

    public CovidConfig()
    {
        try
        {
            BacaKonfigurasi();
        }
        catch (Exception)
        {
            AturDefault();
            TulisKonfigurasiBaru();
        }
    }

    private void BacaKonfigurasi()
    {
        string teksJson = File.ReadAllText(namaFile);
        DataConfig = JsonSerializer.Deserialize<PengaturanCovid>(teksJson);
    }

    private void AturDefault()
    {
        DataConfig = new PengaturanCovid(
            "celcius",
            14,
            "Anda tidak diperbolehkan masuk ke dalam gedung ini",
            "Anda dipersilahkan untuk masuk ke dalam gedung ini"
        );
    }

    private void TulisKonfigurasiBaru()
    {
        var opsi = new JsonSerializerOptions { WriteIndented = true };
        string teksJson = JsonSerializer.Serialize(DataConfig, opsi);
        File.WriteAllText(namaFile, teksJson);
    }

    public void UbahSatuan()
    {
        DataConfig.Satuan = (DataConfig.Satuan == "celcius") ? "fahrenheit" : "celcius";
        TulisKonfigurasiBaru();
    }
}