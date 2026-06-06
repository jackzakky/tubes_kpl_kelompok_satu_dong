using System.Collections.Generic;

namespace tubes_kpl_kelompok_satu_dong
{
    
    public sealed class AppSession
    {
        private static AppSession _instance = null;
        private static readonly object _padlock = new object();

        
        public List<MenuItem> MenuList { get; set; } = new List<MenuItem>();
        //public List<Voucher> ClaimedVouchers { get; set; } = new List<Voucher>();

        private AppSession() { }

        public static AppSession Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppSession();
                    }
                    return _instance;
                }
            }
        }
    }
}