using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktbok
{
    class Repository<T> : IEnumerable<T> 
    {

        private List<T> m_repo = new List<T>();

        public void SparaKontakt (T item)
        {
            m_repo.Add(item);
        }

        public List<Kontakter> HämtaKontakt(string sökPå, string sökVal)
        {
            List<T> kontakt = new List<T>();

            switch (sökVal)
            {
                case ("Förnamn"):
                    kontakt = m_repo.Where(x => (x as Kontakter).Förnamn.ToLower() == sökPå.ToLower()).ToList();
                    break;
                case ("Efternamn"):
                    kontakt = m_repo.Where(x => (x as Kontakter).Efternamn.ToLower() == sökPå.ToLower()).ToList();
                    break;
                case ("Hemnummer"):
                    kontakt = m_repo.Where(x => (x as Kontakter).HemNummer.ToLower() == sökPå.ToLower()).ToList();
                    break;
                case ("Jobbnummer"):
                    kontakt = m_repo.Where(x => (x as Kontakter).JobbNummer.ToLower() == sökPå.ToLower()).ToList();
                    break;
                case ("Kontakttyp"):
                    kontakt = m_repo.Where(x => (x as Kontakter).KontaktTyp.ToString().ToLower() == sökPå.ToLower()).ToList();
                    break;
                default:
                    break;
            }

            return kontakt as List<Kontakter>;
        }

        public void RaderaKontakt(T item)
        {
            m_repo.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T val in m_repo)
            {
                yield return val;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
