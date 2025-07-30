namespace IzinTakipProject.Servisler.Applications
{
    public class TarihYardimcisi
    {
        public static int IsGunuHesapla(DateTime baslangic, DateTime bitis)
        {
            int toplam = 0;
            for (var dt = baslangic; dt <= bitis; dt = dt.AddDays(1))
            {
                if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
                    toplam++;
            }
            return toplam;
        }
    }
}
