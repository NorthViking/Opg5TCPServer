using System;

namespace BogLibrary
{
    public class Bog
    {
        private string _titel;
        private string _forfatter;
        private int _sidetal;
        private string _isbn13;

        public Bog(string titel, string forfatter, int sidetal, string isbn13)
        {
            Titel = titel;
            Forfatter = forfatter;
            Sidetal = sidetal;
            Isbn13 = isbn13;

        }
        public Bog() { }

        public string Titel
        {
            get => _titel;
            set => _titel = value;
        }

        public string Forfatter
        {
            get => _forfatter;
            set
            {
                if (value.Length < 2) throw new ArgumentOutOfRangeException();
                _forfatter = value;
            }
        }

        public int Sidetal
        {
            get => _sidetal;
            set
            {
                if (value < 4 || value > 1000) throw new ArgumentOutOfRangeException();
                _sidetal = value;
            }
        }

        public string Isbn13
        {
            get => _isbn13;
            set
            {
                if (value.Length != 13) throw new ArgumentOutOfRangeException();
                _isbn13 = value;
            }
        }
        public override string ToString()
        {
            return Titel + " " + Forfatter + " " + Sidetal + " " + Isbn13;
        }
    }
}
