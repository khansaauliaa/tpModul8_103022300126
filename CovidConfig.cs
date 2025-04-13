using System.Text.Json;
using System.Text.Json.Serialization;

pubic class CovidConfig { 
 
    public string satuan_suhu { get; set; }
    public int batas_hari_demam { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    private const string configPath = "covidConfig.json";

    private static bool isIntialized = false;

    public CovidConfig()
    {
        if (!isIntialized)
        {
            LoadConfig();
            isIntialized = true;
        }
        try
        {
            string json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<CovidConfig>(json);

            satuan_suhu = config.satuan_suhu;
            batas_hari_demam = config.batas_hari_demam;
            pesan_ditolak = config.pesan_ditolak;
            pesan_diterima = config.pesan_diterima;
        }
        catch {

            SetDefault();
            SaveConfig();
        }
    }
    private void SetDefault()
    {
        satuan_suhu = "C";
        batas_hari_demam = 14;
        pesan_ditolak = "Maaf, Anda tidak diperbolehkan masuk.";
        pesan_diterima = "Selamat datang!";
    }

    private void SaveConfig() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(configPath, json);
    }

    public void UbahSatuan()
    {
        if (satuan_suhu.ToLower() == "celcius")
            satuan_suhu = "fahrenheit";
        else
            satuan_suhu = "celcius";

        SaveConfig();
    }

}
