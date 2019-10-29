using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuanLyHocSinh.Lop
{
    public class NodeL
    {
        public Lop L;
        public NodeL link;
    }
    public class Lop
    {
        #region Cac Thanh phan Du lieu
        private string MaLop;
        private string TenLop;
        private int SiSo;
        #endregion

        #region Cac thuoc tinh
        public string malop
        {
            get { return MaLop; }
            set { MaLop = value; }
        }
        public string tenlop
        {
            get { return TenLop; }
            set { if (TenLop != "") TenLop = value; }
        }
        #endregion
        #region Các thương thức
        public Lop() { }
        //Phương thức thiết lập sao chép
        public Lop(Lop l)
        {
            this.malop = l.malop;
            this.tenlop = l.tenlop;
            this.siso = l.siso;
        }
        public Lop(string malop, string tenlop,int siso)
        {
            this.malop = malop;
            this.tenlop = tenlop;
            this.siso = siso;
         }
        public int siso//XL??? tran stack
        {
            get { return SiSo; }
            set
            {
                SiSo = value;
            }
        }
        //Tinh si so du lieu doc tu tep hocsinh?
        public void nhap()
        {
            Console.Write("nhap ma lop: ");
            malop = Console.ReadLine();
            Console.Write("nhap ten lop: ");
            tenlop = Console.ReadLine();
            siso = 0;
        }
        public void hienthi()
        {
            Console.WriteLine("{0}\t{1}\t{2}",malop,tenlop,siso);
        }
        #endregion
        
    }
    class danhsachlop
    {
        public NodeL ds = new NodeL();//danh sach HS
        int m;//so lop trong truong
        public void hienthi()
        {
            NodeL tg = new NodeL(); tg = this.ds;
            Console.WriteLine("Malop\tTenlop\tSiso");
            if (tg != null)
                while (tg != null)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", tg.L.malop, tg.L.tenlop,tg.L.siso);
                    tg = tg.link;
                }
            else Console.WriteLine("Danh sách trong");
        }
        #region Doc Du lieu tu tep lop.txt va day vao danh sach moc noi don ds; Hien thi
        public void doctep()
        {
            StreamReader fr = File.OpenText("lop.txt");
            ds = null; m = 0;
            NodeL tg;
            string s;
            string[] con;//cac truong DL
            s = fr.ReadLine();//doc dong dau=> duoc ban ghi dau tien
            if (s != null)
            {
                con = new string[3];
                while (s != null)//lan luot doc het tep
                {
                    if (s.Length > 0)
                    {
                        con = s.Split('#');
                        tg = new NodeL(); tg.L = new Lop();
                        tg.L.malop = con[0];
                        tg.L.tenlop = con[1];
                        tg.L.siso = int.Parse(con[2]);
                        //them TT lop vua doc tu tep vao DS lop hoc ds
                        m++;
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
            Console.WriteLine("Du lieu vua doc duoc tu tep LOP.TXT nhu sau:");
            hienthi();
            fr.Close();
        }
        #endregion
        #region them lop va ghi vao cuoi tep
        public void Insert()
        {
            
             
            string malop, tenlop; int siso;
            //nhap ma ko trung
            NodeL tgl=new NodeL();
            bool ok=true;//mặc dịnh ma ko trung
            do{
                ok = true;
                Console.Write("nhap ma lop: ");
                malop = Console.ReadLine();
                tgl=this.ds;
                while(tgl!=null)
                {
                    if (string.Compare(malop, tgl.L.malop) == 0) { ok = false; break; }
                    tgl=tgl.link;
                }
            }while(!ok);
            Console.Write("nhap ten lop: ");
            tenlop = Console.ReadLine();
            siso = 0;
            NodeL tg = new NodeL();
                tg.L= new Lop(malop,tenlop,siso); //???
            ghitep(tg);
            tg.link = null; 
            tg.link = ds;
            ds = tg;
        }
        public void ghitep(NodeL tg)
        {
            StreamWriter fw = File.AppendText("Lop.txt");
            fw.WriteLine();
            fw.Write("{0}#{1}#{2}", tg.L.malop, tg.L.tenlop,tg.L.siso);
            fw.Close();
        }
        #endregion
        #region Sắp xếp cac lop theo khoi va ghi lại vào tệp
        public void hoanvi(NodeL p, NodeL q)
        {
            NodeL tg = new NodeL();
            tg.L = p.L;
            p.L = q.L;
            q.L = tg.L;
        }
        public void Sortkhoi()
        {
            NodeL i, j;
            i = this.ds; j = this.ds.link;
            if (!(i.link == null || i.link.link == null))//DS rong or DS chỉ có 1 nút thì ko phải SX
            {
                while (i.link.link != null)
                {
                    j = i.link;
                    while (j.link != null)
                    {
                        if (string.Compare(i.L.malop, j.L.malop) < 0)
                            hoanvi(i, j);
                        j = j.link;
                    }
                    i = i.link;
                }
            }
            hienthi();
            StreamWriter fw = File.CreateText("Lop.txt");
            i = this.ds;
            while (i != null)
            {
                fw.WriteLine("{0}#{1}#{2}",i.L.malop,i.L.tenlop,i.L.siso);
                i = i.link;
            }
            fw.Close();
        }
        #endregion
        #region Thong ke số lớp theo khối
        public void Thongke()
        {
            StreamWriter ftk;
            if (File.Exists("Thongkekhoi.txt"))
                ftk = File.AppendText("Thongkekhoi.txt");
            else ftk = File.CreateText("Thongkekhoi.txt");
            int d10, d11, d12;
            d10=d11=d12 = 0;
            NodeL tg = this.ds;
            while (tg.link != null)
            {
                if (tg.L.malop.IndexOf("1215")>=0) d12++;
                if (tg.L.malop.IndexOf("1316") >= 0) d11++;
                if (tg.L.malop.IndexOf("1417")>=0) d10++;
                tg = tg.link;
            }
            Console.WriteLine("So lop khoi 10   : {0} ", d10);
            Console.WriteLine("So lop khoi 11   : {0} ", d11);
            Console.WriteLine("So lop khoi 12   : {0} ", d12);
            DateTime dt = DateTime.Now;
            ftk.WriteLine("Thoi diem thong ke: "+ dt.ToString("dd/MM/yyyy"));
            ftk.WriteLine("So lop khoi 10   : {0} ", d10);
            ftk.WriteLine("So lop khoi 11   : {0} ", d11);
            ftk.WriteLine("So lop khoi 12   : {0} ", d12);
            ftk.Close();
        }
        #endregion
    }
    
    
}
