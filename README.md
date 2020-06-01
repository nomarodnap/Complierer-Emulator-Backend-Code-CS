# Complierer-Emulator-Backend-Code-CS

เก็งตัวอย่างข้อสอบ ประจำวิชา COS4504 - COMPILER CONSTRUCTION

อันนี้เป็น backend code ประจำวิชา complierer ของ อ.อภิรักษ์ ซึ่งเป็นวิชาบังคับที่มีชื่อเสียงและหินในมหาวิทยาลัยรามคำแหง ซึ่งเป็นบททดสอบว่านักศึกษานั้นต้องแข็งแรง algorithm & data structure , OOP แค่ไหนถึงจะผ่านวิชานี้ได้
เป็น code ที่ผมหัดเขียนอยู่ที่บ้านก่อนเข้าห้องสอบ ซึ่งโจทย์ในห้องสอบยากและซับซ้อนกว่านี้มากๆ แต่ไม่สามารถเอาโค้ทในห้องสอบมาแสดงให้ได้ แต่หลักการ โครงสร้าง การเขียนก็คล้ายๆกัน
การใช้งานคือ ต้องเปลี่ยนคำสั่งในช่วงที่อยู่ท้ายคำสั่ง code ซึ่งเป็น String แล้วโปรแกรมซึ่งเปรียบหน้าที่เหมือน complier จะแปลงคำสั่งใน String ซึ่งเป็น input แสดง output ออกมาเป็น assembly code ซึ่งถ้าเขียนผิด syntax หรือ semantics complier ก็จะไม่ทำงานและบอก error ตรงส่วนไหน


เปลี่ยน String ตรงฟังชั่นนี้ ท้ายโปรแกรม
        static void Main(string[] args)
        {
            string input =

               "var i,j;" +
               "struct X {" +
               "var  ia, sx;" +
               "}" +
               "struct X a;" +
               "var gi ;" +
               "f()" +
               "{" +
               "var i,j, x ,y ,k ,z;" +
              // "x=z;" +
               "x = x+z*5;"+
               //"gi  = 10 + z;" +
               //"a.ia = 2;" +
               "for x*30 in [ x*2 > , y >= , k*2 =]"+
               " {"
                    +"for x*311111110 in [ x*21111111111 > , y >= , k*2 =]"+
                        "{"
                        +"}"
                +"} " 
               +"}" + "$";    
      
            Program p = new Program();
            p.Lexical(input);
           // p.ShowLexical();
            p.Grammar();

            //p.showIcode();
            p.MachineCode();

            Console.ReadKey();
        }
