using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using QuanLyHocSinh.Lop;
namespace QuanLyHocSinh.Hocsinh
{
    public class Nodehs
    {
        public HocSinh hs;
        public Nodehs link;
    }
    public class HocSinh
    {
        #region Cac Thanh phan Du lieu
        private int MaHS;
        private string HoTen;
        private string GioiTinh;
        public DateTime NamSinh;
        private string Email;
        private double DTB;
        private string MaLop;
        #endregion
        #region Cac thuoc tinh
        public int mahs
        {
            get { return MaHS; }
            set { MaHS = value; }
        }
        public string hoten
        {
            get { return HoTen; }
            set { if (HoTen != "") HoTen = value; }
        }
        public string gioitinh
        {
            get { return GioiTinh; }
            set
            {
                GioiTinh = value.ToLower();
            }
        }
        public DateTime namsinh//XL?
        {
            get { return NamSinh; }
            set
            {
                if (DateTime.Now.Year - NamSinh.Year + 1 >= 15 && DateTime.Now.Year - NamSinh.Year + 1 <= 30)
                    NamSinh = value;
            }
        }
        public string email
        {
            get { return Email; }
            set
            {
                Email = value;    
            }
            
        }
        private bool isEmail(string Email)//XL
        {
            bool ok = true;
            int da = 0, dc = 0, va, vc;
            for (int i = 0; i < Email.Length; i++)
            {
                if (Email[i] == '@') da++;
                if (Email[i] == '.') dc++;
            }
            va = Email.IndexOf("@");
            vc = Email.IndexOf(".");
            if (!(da != 1 && dc >= 1 && va > 0 && va < vc && vc < Email.Length - 1))
                ok = false;
            return ok;
        }
        public string malop
        {
            get { return MaLop; }
            set { if (MaLop != "") MaLop = value; }
        }
        public double dtb
        {
            get { return DTB; }
            set
            {//Làm tròn 1 chữ số sau dấu phẩy
                if (value >= 0 && value <= 10)
                    DTB = Math.Round(value * 10) / 10.0;
            }
        }
        #endregion
        #region Các thương thức thiet lap/nhap/hienthi
        public HocSinh() { }
        //Phương thức   
        public HocSinh(HocSinh hs)
        {
            this.mahs = hs.mahs;
            this.hoten = string.Copy(hs.hoten);
            this.gioitinh = string.Copy(hs.gioitinh);
            this.namsinh = hs.namsinh;
            this.email = string.Copy(hs.email);
            this.dtb = hs.dtb;
            this.malop = string.Copy(hs.malop);
        }
        public HocSinh(int mahs, string hoten, string gioitinh,DateTime namsinh,string email, double dtb,string malop)
        {
            this.mahs = mahs;
            this.hoten = hoten;
            this.gioitinh = gioitinh;
            this.namsinh = namsinh;
            this.email = email;
            this.dtb = dtb;
            this.malop = malop;
        }
        public void hienthi(HocSinh hs)
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", hs.mahs, hs.hoten, hs.gioitinh, hs.namsinh, hs.email, hs.dtb, hs.malop);
        }
        public void nhap()
        {//C1: ma tu tang; C2 Nhap ma va kiem tra trung
            //1.mặc định mã HS ko bị trùng, 2.SV có mã lớp chưa có
            Console.Write("Nhap Ma hoc sinh: ");
            mahs = int.Parse(Console.ReadLine());
            Console.Write("Nhap ho va ten: ");
            hoten = Console.ReadLine();
            Console.Write("Nhap gioi tinh: ");
            gioitinh = Console.ReadLine();
            Console.Write("Nhap ngay/thang/namsinh: ");
            namsinh = DateTime.Parse( Console.ReadLine());
            Console.Write("Nhap ddiaj chir email: ");
            email = Console.ReadLine();
            Console.Write("Nhap diem trung binh: ");
            dtb = double.Parse( Console.ReadLine());
            Console.Write("Nhap ma lop: ");
            malop = Console.ReadLine();
        }
        public void nhap1()
        {//C1: ma tu tang; C2 Nhap ma va kiem tra trung
            //1.mặc định mã HS ko bị trùng, 2.SV có mã lớp chưa có
            Console.Write("Nhap ho va ten: ");
            hoten = Console.ReadLine();
            Console.Write("Nhap gioi tinh: ");
            gioitinh = Console.ReadLine();
            Console.Write("Nhap ngay/thang/namsinh: ");
            namsinh = DateTime.Parse(Console.ReadLine());
            Console.Write("Nhap ddiaj chir email: ");
            email = Console.ReadLine();
            Console.Write("Nhap diem trung binh: ");
            dtb = double.Parse(Console.ReadLine());
        }
        #endregion
    }
    public class DanhSachHocSinh
    {
        public Nodehs ds=new Nodehs();//danh sach HS
        int n;//so HS
        //StreamWriter fw = File.CreateText("TKhocsinh.txt");
        #region Doc Du lieu tu tep hocsinh.txt va day vao danh sach moc noi don ds; Hien thi
        public void doctep()
        {
            StreamReader fr = File.OpenText("hocsinh.txt");
            ds = null; n = 0;
            Nodehs tg;
            string s;
            string[] con;//cac truong DL
            s = fr.ReadLine();//doc dong dau=> duoc ban ghi dau tien
            if (s != null)
            {
                int d = 0;//d so truong DL, d=so dau #+1
                for (int i = 0; i < s.Length; i++)
                    if (s[i] == '#') d++;
                con = new string[d + 1];
                
                while (s != null)//lan luot doc het tep
                {
                    if (s.Length > 0)
                    {
                        con = s.Split('#');
                        tg = new Nodehs(); tg.hs = new HocSinh();
                        tg.hs.mahs = int.Parse(con[0]);
                        tg.hs.hoten = con[1];
                        tg.hs.gioitinh = con[2];
                        tg.hs.namsinh = DateTime.Parse(con[3]);
                        tg.hs.email = con[4];
                        tg.hs.dtb = double.Parse(con[5]);
                        tg.hs.malop = con[6];
                        //them TT SV vua doc tu tep vao DS SV ds
                        n++;
                        if (ds == null) ds = tg;
                        else
                        {
                            tg.link = ds;
                            ds = tg;
                        }
                     
                        s = fr.ReadLine();
                    }
                    
                }
            }
            else Console.WriteLine("tep rong!!!");
            Console.WriteLine("Du lieu vua doc duoc tu tep HOCSINH.TXT nhu sau:");
            hienthi();
            fr.Close();
        }
        #endregion
        public void hienthi()
        {
            Nodehs tg = new Nodehs();tg= this.ds;
            Console.WriteLine("MaHS\tHoTen\tGioiTinh\tNamSinh\tEmail\tDTB\tMaLop");
            if (tg != null)
                while (tg != null)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", tg.hs.mahs, tg.hs.hoten, tg.hs.gioitinh, tg.hs.namsinh, tg.hs.email, tg.hs.dtb, tg.hs.malop);
                    //tg.hs.hienthi(tg.hs);
                    tg = tg.link;
                }
            else Console.WriteLine("Danh sách trong");
        }
        #region them hocsinh va ghi vao cuoi tep HocSinh.txt
        public void Insert()
        {
            Nodehs tg = new Nodehs();
            tg.hs = new HocSinh(); tg.link = null;
            
            //??Kiem tra ma lop da co chua? Neu chua=> bao loi; neu co: 1 ghi them SV vao tep hocsinh.txt; cap nhat siso tep Lop.txt?
            //TT HS hop le thi them vao DS HS va ghi vao tep
            Nodehs tghs = new Nodehs();
            danhsachlop dsl = new danhsachlop(); dsl.doctep();
            NodeL tgl = new NodeL();
            int mahs; string malop;
            bool ok1;
            bool ok2;//SV có mã lớp chưa có
            do
            {
                ok1 = true;
                Console.Write("Nhap Ma hoc sinh: ");
                mahs = int.Parse(Console.ReadLine());
                tghs = this.ds;
                while (tghs != null)//XL
                {
                    if (tghs.hs.mahs == mahs) ok1 = false;
                    tghs = tghs.link;
                }
                if (!ok1) Console.WriteLine("DL ko hop le: Ma HS da co! Moi nhap lai");
                
            } while (!ok1);
            tg.hs.mahs=mahs; 
            tg.hs.nhap1();
            do{
                ok2 = false;
                Console.Write("Nhap ma lop: ");
                malop = Console.ReadLine();
                tgl = dsl.ds;
                while (tgl != null)
                {
                    if (string.Compare(tgl.L.malop,malop)==0) { ok2 = true; break; }
                    tgl = tgl.link;
                }
                if (!ok2) Console.WriteLine("DL ko hop le: Ma lop chua co! Moi nhap lai");
            } while (!ok2);//XL

            tg.hs.malop=String.Copy(malop);
            ghitep(tg);
            //Cap nhat si so tep Lop.txt
            tg.link = ds;
            ds = tg;
            
        }
        //Ghi them thong tin vao cuoi tep
        public void ghitep(Nodehs tg)
        {
            StreamWriter fw = File.AppendText("HocSinh.txt");
            fw.WriteLine();
            fw.Write("{0}#{1}#{2}#{3}#{4}#{5}#{6}", tg.hs.mahs, tg.hs.hoten, tg.hs.gioitinh, tg.hs.namsinh, tg.hs.email, tg.hs.dtb, tg.hs.malop);
            fw.Close();
        }
        #endregion
        #region Tim kiem hocsinh theo ten/ma
        public void SearchMa(int ma)
        {
            Nodehs tg = ds;
            while (tg != null)
            {
                if (tg.hs.mahs == ma)
                {tg.hs.hienthi(tg.hs); break;}
                tg = tg.link;
            }
        }
        public void SearchTen(string ten)
        {
            Nodehs tg = ds;
            while (tg != null)
            {
                if (tg.hs.hoten.IndexOf(ten)>=0)
                    tg.hs.hienthi(tg.hs);
                tg = tg.link;
            }
        }

        public void Search()
        {
            Console.Write("Nhap 0/1 neu tim theo ma/ten");
            int kw;
            do{
                kw=int.Parse(Console.ReadLine());
            }while(!(kw==0||kw==1));
            switch (kw)
            {
                case 0:
                    Console.Write("nhap ma HS can tim:");
                    int ma = int.Parse(Console.ReadLine()); 
                    SearchMa(ma); break;
                case 1:
                    Console.Write("nhap ten HS can tim:");
                    string ten = Console.ReadLine();
                    SearchTen(ten); break;
            }
            
        }
        #endregion
        #region Sắp xếp HS theo lớp/điểm
        public void hoanvi(Nodehs p, Nodehs q)
        {
            Nodehs tg=new Nodehs();
            tg.hs = p.hs;
            p.hs = q.hs;
            q.hs = tg.hs;
        }
        public void SortLop()//????ds=null
        {
            Nodehs i,j;
            i = this.ds; j= this.ds.link;
            if (!(i.link == null || i.link.link == null))//DS rong or DS chỉ có 1 nút thì ko phải SX
            {
                while (i.link.link != null)
                {
                    j = i.link;
                    while (j.link != null)
                    {
                        if (string.Compare(i.hs.malop, j.hs.malop) < 0)
                            hoanvi(i, j);
                        j = j.link;
                    }
                    i = i.link;
                }
            }
            hienthi();
        }
        public void SortDiem()
        {
            Nodehs i, j; i = new Nodehs(); j = new Nodehs();
            i = this.ds; j = this.ds.link;
            if (!(i.link == null || i.link.link == null))
            {
                while (i.link.link != null)
                {
                    j = i.link;
                    while (j.link != null)
                    {
                        if (i.hs.dtb < j.hs.dtb)
                            hoanvi(i, j);
                        j = j.link;
                    }
                    i = i.link;
                }
            }
            hienthi();
        }

        public void Sort()
        {
            Console.Write("Nhap 0/1 neu sap xep HS theo lop/diem");
            int kw;
            do
            {
                kw = int.Parse(Console.ReadLine());
            } while (!(kw == 0 || kw == 1));
            switch (kw)
            {
                case 0: SortLop();  break;
                case 1: SortDiem(); break;
            }

        }
        #endregion
        #region Thong ke ty le Yeu (<5)TB(>=5)kha(>=7) gioi(>=8) hocsinh theo ten/ma
        public void ThongkeTyLe()
        {
            StreamWriter ftk;
            if(File.Exists("ThongkeTyLe.txt"))
                ftk=File.AppendText("ThongkeTyLE.txt");
            else  ftk=File.CreateText("ThongkeTyLE.txt");
            int dg, dk, dtb, dy;
            dg = dk = dtb = dy = 0;
            Nodehs tg=this.ds;
            while (tg.link != null)
            {
                if (tg.hs.dtb >= 8) dg++;
                else if (tg.hs.dtb >= 7) dk++;
                else if (tg.hs.dtb >= 5) dtb++;
                else dy++;
                tg = tg.link;
            }
            Console.WriteLine("Ty le Gioi (8..10)   : {0} / {1} ",dg,this.n);
            Console.WriteLine("Ty le Kha (7..8)     : {0} / {1} ", dk, this.n);
            Console.WriteLine("Ty le TrungBinh(5..7): {0} / {1} ", dtb, this.n);
            Console.WriteLine("Ty le Yeu (<5)       : {0} / {1} ", dy, this.n);
            DateTime dt=new DateTime();
            ftk.WriteLine("Thoi diem thong ke: {0}/{1}/{2}",dt.Day,dt.Month,dt.Year);
            ftk.WriteLine("Ty le Gioi (8..10)   : {0} / {1} ", dg, this.n);
            ftk.WriteLine("Ty le Kha (7..8)     : {0} / {1} ", dk, this.n);
            ftk.WriteLine("Ty le TrungBinh(5..7): {0} / {1} ", dtb, this.n);
            ftk.WriteLine("Ty le Yeu (<5)       : {0} / {1} ", dy, this.n);
            ftk.Close();
        }
        #endregion
    }
}
