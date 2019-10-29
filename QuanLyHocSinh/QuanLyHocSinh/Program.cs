using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLyHocSinh.Hocsinh;
using QuanLyHocSinh.Lop;

namespace QuanLyHocSinh
{
    class Program
    {
        static DanhSachHocSinh dshs=new DanhSachHocSinh();
        static danhsachlop dsl = new danhsachlop();
        static void Menu()
        {
            string[] mn ={
                            " 1.Nhap danh sach hoc sinh tu tep hocsinh.txt va lop.txt",
                            " 2.Hien thi danh sach hoc sinh theo lop",
                            " 3.Quan Ly lop hoc",
                            " 4.Them hoc sinh ",
                            " 5.Sua thong tin hoc sinh ",
                            " 6.Xoa thong tin hoc sinh ",
                            " 7.Tim kiem hoc sinh ",
                            " 8.Sap xep hoc sinh theo diem ",
                            " 9.Thong Ke ",
                            " 0.Ket thuc "
                        };
            
            do{
                Console.Clear();
                foreach(string con in mn)
                    Console.WriteLine("\t"+con);
                Console.Write("\t\tBan chon:");
                int chon;
                
                chon=int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1: dshs.doctep(); dsl.doctep(); Console.Write("Ban vua doc DL xong!"); Console.ReadKey(); break;
                    case 2: dshs.hienthi(); Console.ReadKey(); break;//XL???
                    case 3: dsl.Insert(); Console.ReadKey(); break;
                    case 4: dshs.Insert(); Console.ReadKey(); break;
                    case 5: Console.ReadKey(); break;
                    case 6: Console.ReadKey(); break;
                    case 7: dshs.Search(); Console.ReadKey(); break;
                    case 8: dshs.Sort(); dsl.Sortkhoi(); Console.ReadKey(); break;
                    case 9: dshs.ThongkeTyLe(); dsl.Thongke(); Console.ReadKey(); break;
                    case 0: Environment.Exit(0); break;
                }
            }while(true);
        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
