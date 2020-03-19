using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5005019350_Summer
{
    class Program
    {
        char charArray;
        int count = 0;
        ArrayList tokenlist = new ArrayList();
        public struct token { public string word, clss;}

        void Lexical(string input)
        {
            charArray = input[0];
            token ww = new token();
            while (charArray != '$')
            {
                string qq = "";
                charArray = input[count++];
                switch (charArray)
                {
                    case ' ': break;
                    case '+':
                        ww.word = "+";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '-':
                        ww.word = "-";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '*':
                        ww.word = "*";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '/':
                        ww.word = "/";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '=':
                        ww.word = "=";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '[':
                        ww.word = "[";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case ']':
                        ww.word = "]";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case ')':
                        ww.word = ")";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '(':
                        ww.word = "(";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '{':
                        ww.word = "{";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '}':
                        ww.word = "}";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case ';':
                        ww.word = ";";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case ',':
                        ww.word = ",";
                        ww.clss = "OPR";
                        tokenlist.Add(ww);
                        break;
                    case '$':
                        ww.word = "$";
                        ww.clss = "END";
                        tokenlist.Add(ww);
                        break;
                    case '>':
                        qq += charArray + "";
                        charArray = input[count++];
                        qq += charArray + "";
                        if (qq == ">=")
                        {
                            ww.word = ">=";
                            ww.clss = "OPR";
                            tokenlist.Add(ww);
                        }
                        else
                        {
                            ww.word = ">";
                            ww.clss = "OPR";
                            tokenlist.Add(ww);
                            count--;
                        }
                        break;


                    default:
                        if (charArray >= '0' && charArray <= '9')
                        {
                            while (charArray >= '0' && charArray <= '9')
                            {
                                qq +=charArray + "";
                                charArray = input[count++];
                            }
                            if (charArray == '.')
                            {
                                qq +=charArray + "";
                                charArray = input[count++];
                                while (charArray >= '0' && charArray <= '9')
                                {
                                    qq +=charArray + "";
                                    charArray = input[count++];
                                }
                                ww.word = qq;
                                ww.clss = "FLOAT_CONST";
                                tokenlist.Add(ww);
                            }
                            else
                            {
                                ww.word = qq;
                                ww.clss = "INT_CONST";
                                tokenlist.Add(ww);
                            }
                            count--;
                        }
                        else if (charArray == '.')
                        {
                            qq +=charArray + "";
                            charArray = input[count++];
                            while (charArray >= '0' && charArray <= '9')
                            {
                                qq +=charArray + "";
                                charArray = input[count++];
                            }
                            ww.word = qq;
                            ww.clss = "FLOAT_CONST";
                            tokenlist.Add(ww);

                            count--;
                        }


                        else if (charArray >= 'a' && charArray <= 'z' || charArray >= 'A' && charArray <= 'Z')
                        {
                            {
                                qq += charArray + "";
                                charArray = input[count++];
                                while (charArray >= 'a' && charArray <= 'z' || charArray >= 'A' && charArray <= 'Z' || charArray >= '0' && charArray <= '9' || charArray == '_')
                                {
                                    qq +=charArray + "";
                                    charArray = input[count++];
                                }
                                if (charArray == '.')
                                {
                                    qq += charArray + "";
                                    charArray = input[count++];
                                    while (charArray >= 'a' && charArray <= 'z' || charArray >= 'A' && charArray <= 'Z' || charArray >= '0' && charArray <= '9' || charArray == '_')
                                    {
                                        qq += charArray + "";
                                        charArray = input[count++];
                                    }
                                }

                                if (qq == "var" || qq == "for" || qq == "in" || qq == "struct")
                                {
                                    ww.word = qq;
                                    ww.clss = "RESV";
                                    tokenlist.Add(ww);
                                }
                                else
                                {
                                    ww.word = qq;
                                    ww.clss = "ID";
                                    tokenlist.Add(ww);
                                }
                            }
                            count--;
                        }
                        break;
                }

            }
        }


        void ShowLexical()
        {
            foreach (token a in tokenlist)
            {
                Console.WriteLine("{0}\t{1}" , a.word ,a.clss); 
            }
        }







        /***************************Grammar**********************************/

        token now = new token();
        bool b;

        bool Grammar()
        {
            count = 0;
            now = (token)tokenlist[count++];
            b = S(); if (b == false)
            {
                Console.Error.WriteLine("Grammar false : {0}", now.word);
                return false; 
            }
            return true;
        }



        //S -> T F

        bool S()
        {
            pos = "Global";
            b = T(pos); if (b == false) return false;
            b = F(); if (b == false) return false;
            if (now.word == "$") return true;
            return false;
        }

        //T -> T T1 | e
        bool T(string pos)
        {
            // if (e)
            if (now.clss == "ID" ) return true; else
                if (now.word == "$" || now.word == "for") return true;
                else

            b = T1(); if (b == false) return false;
            b = T(pos); if (b == false) return false;

            return true;
        }

        //T1 -> var id T2; | R1 | R2
        bool T1()
        {
            if (now.word == "var")
            {
                now = (token)tokenlist[count++];
                if (now.clss == "ID")
                {
                    if (now.word[0] == 'i' || now.word[0] == 'j' || now.word[0] == 'k')
                    { type = "INT"; }
                    else if (now.word[0] == 'f')
                    { type = "FLOAT"; }
                    else if (now.word[0] == 's')
                    { type = "STRING"; }
                    else type = "ANY";
                    b = Declare(pos, type, now); if (b == false)  return b;

                    now = (token)tokenlist[count++];
                }
                else return false;
                if (now.word == "," || now.word == ";")
                {
                    b = T2(); if (b == false) return false;
                    if (now.word == ";") 
                    {
                        now = (token)tokenlist[count++]; 
                        return true;
                    } 
                    else return false;
                }
                else return false;
            }

            else if (now.word == "struct") { b = R1(); return b; }
            
                else return false;
        }

        //T2 -> ,id | e
        bool T2()
        {
            if (now.word == ";") return true;
            else if (now.word == ",")
            {
                now = (token)tokenlist[count++];
                if (now.clss == "ID")
                {
                    if (now.word[0] == 'i' || now.word[0] == 'j' || now.word[0] == 'k')
                    { type = "INT"; }
                    else if (now.word[0] == 'f')
                    { type = "FLOAT"; }
                    else if (now.word[0] == 's')
                    { type = "STRING"; }
                    else type = "ANY";
                    b = Declare(pos, type, now); if (b == false)  return b;

                    now = (token)tokenlist[count++];
                    b = T2(); return b;
                }
                else return false;
            }
            return false;
        }

        //R1 -> struct X R2
        bool R1()
        {
            if (now.word == "struct")
            {
                now = (token)tokenlist[count++];
                if (now.word == "X" || now.word == "Y" )
                {
                    now = (token)tokenlist[count++];
                        b = R2();
                        pos = "Global";
                    return b;
                }
                else return false;
            }
            else return false;
        }

        //R2 -> { T3 }
        //R2 -> id T2 ;

        bool R2()
        {
            if (now.word == "{")
            {
                now = (token)tokenlist[count++];
                b = T3(); if (b == false) return b;
                if (now.word == "}")
                {
                    now = (token)tokenlist[count++]; return true;
                }
                else return false;
            }
            else if (now.clss == "ID")
            {
                type = "STRUCT";
                b = Declare(pos, type, now); if (b == false) return b;


                now = (token)tokenlist[count++];
                b = T2(); if (b == false) return b;
                if (now.word == ";")
                {
                    now = (token)tokenlist[count++]; return true;
                }
                else return false;
            }
            else return false;
        }

        
        //T3 -> var id T2;
        bool T3()
        {
            pos = "Struct";
            if (now.word == "var")
            {
                now = (token)tokenlist[count++];
                if (now.clss == "ID")
                {

                    if (now.word[0] == 'i' || now.word[0] == 'j' || now.word[0] == 'k')
                    { type = "INT"; }
                    else if (now.word[0] == 'f')
                    { type = "FLOAT"; }
                    else if (now.word[0] == 's')
                    { type = "STRING"; }
                    else type = "ANY";
                    b = Declare(pos, type, now); if (b == false)  return b;

                    now = (token)tokenlist[count++];
                    if (now.word == ",")
                    {
                        now = (token)tokenlist[count++];
                        if (now.clss == "ID")
                        {
                            if (now.word[0] == 'i' || now.word[0] == 'j' || now.word[0] == 'k')
                            { type = "INT"; }
                            else if (now.word[0] == 'f')
                            { type = "FLOAT"; }
                            else if (now.word[0] == 's')
                            { type = "STRING"; }
                            else type = "ANY";
                            b = Declare(pos, type, now); if (b == false)  return b;

                            now = (token)tokenlist[count++];
                            b = T2(); if (b == false) return false;
                            if (now.word == ";")
                            {
                                now = (token)tokenlist[count++]; 
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        //F -> id (){ T S2}
        bool F()
        {
            if (now.clss == "ID")
            {
                now = (token)tokenlist[count++];
                if (now.word == "(")
                {
                    now = (token)tokenlist[count++];
                    if (now.word == ")")
                    {
                        now = (token)tokenlist[count++];
                        if (now.word == "{")
                        {
                            now = (token)tokenlist[count++];
                            pos = "Local";
                                b = T(pos); if (b==false) return b;
                                b = S2(); if (b==false) return b;
                                if (now.word == "}")
                                {
                                    now = (token)tokenlist[count++]; return true;
                                }
                                else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        //S2 -> A S2 | F2 S2
        bool S2()
        {
            if (now.clss == "ID")
            {
                b = A(); if (b == false) return false;
                b = S2(); return b;
            }
            else if (now.word == "for")
            {
                b = F2(); if (b == false) return false;
                b = S2(); return b;
            }
            else if (now.word == "}" ) return true;
            else return false;
        }

        token rr,ee;
        //A -> id = E1;
        bool A()
        {
            Infix = new ArrayList(); 
            if (now.clss == "ID")
            {
                rr = now;

                MatchDataType.Add(CheckDataType(now));

                now = (token)tokenlist[count++];
                if (now.word == "=")
                {
                    ee = now;
                    now = (token)tokenlist[count++];
                    b = E1(); if (b == false) return false;
                    if (now.word == ";") 
                    {
                        b = CheckAllDataType(); if (b == false) return b;
                        now = (token)tokenlist[count++]; 
                         return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        //I -> id | int_const | float_const | string_const
        bool I()
        {
            if (now.clss == "ID" || now.clss == "INT_CONST" || now.clss == "FLOAT_CONST")
                {
                    
                    MatchDataType.Add(CheckDataType(now));
                    Infix.Add(now);


                  now = (token)tokenlist[count++]; return true;
                 }
            else return false;
        }

        //E1 -> I E2
        bool E1()
        {
            
            if (now.clss == "ID" || now.clss == "INT_CONST" || now.clss == "FLOAT_CONST")
            {
                b = I(); if (b == false) return b;
                b = E2(); return b;



            }
            else return false;
        }

        //E2 -> O1 E1 | e
        bool E2()
        {
             if (now.word == "+" || now.word == "-" || now.word == "*" || now.word == "/")
            {
                b = O1(); if (b == false) return b;
                b = E1(); return b;
                
            }
            else if (now.word == ";" || now.word == "in" )
            {
                
                InfixToPostfix(Infix);
                Postfix.Add(ee);
                Postfix.Insert(0, rr);
                PostfixTo_4_tuple(Postfix);
                Postfix = new ArrayList();


                return true;
            }
             else if (now.word == ">" || now.word == ">=" || now.word == "=")
            {
                

                InfixToPostfix(Infix);
                Postfix.Add(ee);
                Postfix.Insert(0, rr);
                PostfixTo_4_tuple(Postfix);
                Postfix = new ArrayList();
                Infix = new ArrayList();

                return true;
            }
            else return false;
        }

        string yatta;
        //F2 -> for E in [ E O N ] { S }
        bool F2()
        {
            Infix = new ArrayList(); 
            if (now.word == "for")
            {

                InterCode a = new InterCode();
                a.op = "LBL";
                a.opr1 = "-";
                a.opr2 = "-";
                a.res = "L" + countL++; ;
                string xxcv = a.res;
                Icode.Add(a);

                int asdfg = countT + 1;

                yatta = "T" + asdfg;

                now = (token)tokenlist[count++];
                b = E1(); if (b==false) return b;

                b = CheckAllDataType(); if (b == false) return b;


                if (now.word == "in")
                {

                    Infix = new ArrayList();
                    now = (token)tokenlist[count++];
                    if (now.word == "[")
                    {

                        now = (token)tokenlist[count++];
                        E1(); if (b==false) return b;
                        O(); if (b==false) return b;
                        b = CheckAllDataType(); if (b == false) return b;
                        N(); if (b==false) return b;
                        if (now.word == "]")
                        {
                            InterCode q = new InterCode();
                            q.op = "JMP";
                            q.opr1 = "-";
                            q.opr2 = "-";
                            int sadxc = countL + 1;
                            q.res = "L" + sadxc;
                            Icode.Add(q);



                            now = (token)tokenlist[count++];
                            if (now.word == "{")
                            {
                                a.op = "LBL";
                                a.opr1 = "-";
                                a.opr2 = "-";
                                a.res = "L" + countL++; ;
                                Icode.Add(a);


                                now = (token)tokenlist[count++];
                                S2(); if (b==false) return b;
                                if (now.word == "}")
                                {
                                    InterCode x = new InterCode();
                                    x.op = "JMP";
                                    x.opr1 = "-";
                                    x.opr2 = "-";
                                    x.res = xxcv;
                                    Icode.Add(x);
                                    
                                    x.op = "LBL";
                                    x.opr1 = "-";
                                    x.opr2 = "-";
                                    x.res = "L" + countL++;
                                    Icode.Add(x);

                                    now = (token)tokenlist[count++];
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }
        //N -> , E O | e
        bool N()
        {
            if (now.word == "]") return true;
            else if (now.word == ",")
            {
                now = (token)tokenlist[count++];
                if (now.clss == "ID" || now.clss == "INT_CONST" || now.clss == "FLOAT_CONST")
                {
                    b= E1(); if (b == false) return b;
                    b = O(); if (b == false) return b;
                    b = N(); return b;
                }
                else return false;
            }
            else return false;

        }

        //O -> > , >= , =
        bool O()
        {
            int countTT = countT - 1;
            int countLL = countL - 1;
            
            if (now.word == ">")
            {
                InterCode a = new InterCode();
                a.op = ">";
                a.opr1 = "T" + countT;
                a.opr2 = yatta;
                a.res = "-";
                Icode.Add(a);

                a.op = "JT";
                a.opr1 = "-"; 
                a.opr2 = "-";
                a.res = "L"+countL;
                Icode.Add(a);


                now = (token)tokenlist[count++]; return true; 
            }
            if (now.word == ">=")
            {
                

                InterCode b = new InterCode();
                b.op = ">=";
                b.opr1 = "T" + countT;
                b.opr2 = yatta;
                b.res = "-";
                Icode.Add(b);

                b.op = "JT";
                b.opr1 = "-";
                b.opr2 = "-";
                b.res = "L" + countL;
                Icode.Add(b);

                now = (token)tokenlist[count++]; return true; 
            }

            if (now.word == "=")
            {
                InterCode c = new InterCode();
                c.op = "==";
                c.opr1 = "T" + countT;
                c.opr2 = yatta;
                c.res = "-";
                Icode.Add(c);

                c.op = "JT";
                c.opr1 = "-";
                c.opr2 = "-";
                c.res = "L" + countL;
                Icode.Add(c);

                now = (token)tokenlist[count++]; return true;
            }



            else return false;

        }

        //O1 -> +,-,*,/
        bool O1()
        {
            if (now.word == "+" || now.word == "-" || now.word == "*" || now.word == "/")
            { 
            Infix.Add(now);
                now = (token)tokenlist[count++]; return true; }
            else return false;
        }


        /*************semetic************/
        

        char v;

        string pos, type;

        ArrayList StructArray = new ArrayList();
        ArrayList GlobalArray = new ArrayList();
        ArrayList LocalArray = new ArrayList();

        ArrayList MatchDataType = new ArrayList();

        struct Variable
        {
            public token token;
            public string type;
        }


        bool Declare(string pos, string type, token now)
        {
            if (pos == "Struct")
            {
                foreach (Variable a in StructArray)
                {
                    if (now.word == a.token.word)
                    {
                        Console.Error.WriteLine("Struct Variable Dupplicate : {0}", now.word);
                        return false;
                    }
                }
                Variable qq = new Variable();
                qq.token = now;
                qq.type = type;
                StructArray.Add(qq);
                return true;
            }
            else if (pos == "Global")
            {
                foreach (Variable a in GlobalArray)
                {
                    if (now.word == a.token.word)
                    {
                        Console.Error.WriteLine("Global Variable Dupplicate : {0}", now.word);
                        return false;
                    }
                }
                Variable qq = new Variable();
                qq.token = now;
                qq.type = type;
                GlobalArray.Add(qq);
                return true;
            }
            else if (pos == "Local")
            {
                foreach (Variable a in LocalArray)
                {
                    if (now.word == a.token.word)
                    {
                        Console.Error.WriteLine("Local Variable Dupplicate : {0}", now.word);
                        return false;
                    }
                }
                Variable qq = new Variable();
                qq.token = now;
                qq.type = type;
                LocalArray.Add(qq);
                return true;
            }
            else return false;
        }



        public struct token4check { public string word, type;}

        token4check CheckDataType(token now)
        {
            token4check qwer = new token4check();

            if (now.clss == "INT_CONST")
            {
                qwer.word = now.word;
                qwer.type = "INT";
                return qwer;
            }
            if (now.clss == "FLOAT_CONST")
            {
                qwer.word = now.word;
                qwer.type = "FLOAT";
                return qwer;
            }
            if (now.clss == "STRING_CONST")
            {
                qwer.word = now.word;
                qwer.type = "FLOAT";
                return qwer;
            }
                if (now.clss == "ID")
                {
                    
                    if (checkStruct() == true)
                    {
                        foreach (Variable a in StructArray)
                        {
                            if (this.now.word == a.token.word)
                            {
                                qwer.word = now.word;
                                qwer.type = a.type;
                                return qwer;
                            }
                        }
                        return qwer;
                    } else
                    foreach (Variable b in GlobalArray)
                    {
                        if (now.word == b.token.word)
                        {
                            qwer.word = now.word;
                            qwer.type = b.type;
                            return qwer;
                        }
                    }
                    foreach (Variable c in LocalArray)
                    {
                        if (now.word == c.token.word)
                        {
                            qwer.word = now.word;
                            qwer.type = c.type;
                            return qwer;
                        }
                    }
                    Console.Error.WriteLine("Variable not Declare : {0}", now.word);
                    return qwer;
                }
            Console.Error.WriteLine("Variable not Declare : {0}", now.word);
            return qwer;
        }


        
        bool CheckAllDataType()    {
            token4check xxx = (token4check)MatchDataType[0];
                        for (int i = 1 ; i < MatchDataType.Count; i++)
                        {
                            token4check yyy = (token4check)MatchDataType[i];
                            if (xxx.type == yyy.type)
                            {
                                xxx.type = yyy.type;
                            }
                            else if (xxx.type == "ANY" || yyy.type == "ANY")
                            {
                                xxx.type = yyy.type;
                            }
                            else
                            {
                                Console.Error.WriteLine("Variable not match");
                                return false;
                            }
                        }

                        string asd="";
                        foreach (token4check a in MatchDataType)
                        {
                            if (a.type != "ANY")
                                asd = a.type;
                        }


                        Variable yay = new Variable();
            
                        foreach (token4check a in MatchDataType)
                        {

                            
                            if (a.type == "ANY")
                            {
                                int ppp = 999; bool qqq = false; int ooo = 0;
                                foreach (Variable b in GlobalArray)
                                {
                                     ooo++;
                                    if (a.word == b.token.word)
                                    {
                                        yay.token.word = b.token.word;
                                        yay.token.clss = b.token.clss;
                                        yay.type = asd;
                                         ppp = ooo;
                                         qqq = true;
                                    }
                                }
                                if (qqq) 
                                {
                                    GlobalArray.Insert(ppp, yay); 
                                    GlobalArray.RemoveAt(ppp - 1); 
                                }

                                ppp = 999;  qqq = false;
                                ooo = 0;
                                foreach (Variable c in LocalArray)
                                {
                                    ooo++;
                                    if (now.word == c.token.word)
                                    {
                                        yay.token.word = c.token.word;
                                        yay.token.clss = c.token.clss;
                                        yay.type = asd;
                                        LocalArray.Insert(ooo, yay);
                                        ppp = ooo ;
                                        qqq = true;
                                    }
                                }
                                if (qqq)
                                {
                                    LocalArray.Insert(ppp, yay);
                                    LocalArray.RemoveAt(ppp - 1);
                                }
                            }
                        }

                         MatchDataType = new ArrayList();
                         return true;
        }


        bool checkStruct()
        {
            for (int i = 1; i < now.word.Length; i++)
            {
                v = now.word[i];
                if (v == '.')
                {
                    string newvar = "";
                    for (int j = i + 1; j < now.word.Length; j++)
                    { newvar += now.word[j] + ""; }

                    string qqqq = "";
                    for (int j = 0; j < i; j++)
                    { qqqq += now.word[j] + ""; }
                    foreach (Variable b in GlobalArray)
                    {
                        if (qqqq == b.token.word && b.type == "STRUCT")
                        {
                            now.word = newvar;
                            return true;
                        }
                    }
                    foreach (Variable c in LocalArray)
                    {
                        if (qqqq == c.token.word && c.type == "STRUCT")
                        {
                            now.word = newvar;
                            return true;
                        }
                    }
                    return false;
                }
            } return false;
        }



        /**************intercode infix postfix***************/


                struct InterCode
        {
            public string op , opr1 , opr2 , res;
        }

        int countT = 0;
        int countL = 1;

        ArrayList Postfix = new ArrayList();
        ArrayList Infix = new ArrayList();
        ArrayList Icode = new ArrayList();
        


        ArrayList InfixToPostfix(ArrayList Infix)
        {
            Postfix = new ArrayList();
            int prio = 0;
            Stack<token> s1 = new Stack<token>();
            for (int i = 0; i < Infix.Count; i++)
            {
                token ch = (token)Infix[i];
                if (ch.word == "+" || ch.word == "-" || ch.word == "*" || ch.word == "/")
                {
                    if (s1.Count <= 0)
                        s1.Push(ch);
                    else
                    {
                        if (s1.Peek().word == "*" || s1.Peek().word == "/")
                            prio = 1;
                        else
                            prio = 0;
                        if (prio == 1)
                        {
                            Postfix.Add(s1.Pop());
                            i--;
                        }
                        else
                        {
                            if (ch.word == "+" || ch.word == "-")
                            {
                                Postfix.Add(s1.Pop());
                                s1.Push(ch);
                            }
                            else
                                s1.Push(ch);
                        }
                    }
                }
                else
                {
                    Postfix.Add(ch);
                }
            }
            int len = s1.Count;
            for (int j = 0; j < len; j++)
            {
                token x = s1.Pop();
                Postfix.Add(x);
            }

            return Postfix;
        }



        void PostfixTo_4_tuple(ArrayList Postfix)
        {



            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < Postfix.Count; i++)
            {
                


                token ch = (token)Postfix[i];
                if (ch.clss != "OPR")
                    stack.Push(ch.word);
                else
                {
                    if (ch.word == "=")
                    {
                        InterCode a = new InterCode();
                        a.op = "=";
                        a.opr1 = stack.Pop();
                        a.opr2 = "-";
                        a.res = stack.Pop();
                        Icode.Add(a);
                    }
                    else
                    {
                        string opr2 = stack.Pop(); string opr1 = stack.Pop();
                        InterCode b = new InterCode();
                        b.op = ch.word;
                        b.opr1 = opr1;
                        b.opr2 = opr2;
                        countT++;
                        b.res = "T" + countT;
                        Icode.Add(b);
                        stack.Push("T" + countT);
                    }
                }
            }
        }





        struct LBLstruct { public string Number , Address ;}
        ArrayList LBLarray = new ArrayList();



        void MachineCode()
        {
            int line = 0;
            foreach (InterCode a in Icode)
            {
                switch (a.op)
                {

                    // LBL 10
                    case "LBL":
                        LBLstruct d = new LBLstruct();
                        d.Number = a.res;
                        d.Address = "" + line;
                        LBLarray.Add(d);
                        break;

                    // JT 10
                    case "JT":
                        line++;
                        break;

                    // JMP 10
                    case "JMP":
                        line++;
                        break;

                    // LOD R,
                    // STO R,
                    case "=":
                        line++;
                        line++;
                        break;

                    // LOD R,
                    // ADD R,
                    // STO R,
                    case "+":
                        line++;
                        line++;
                        line++;
                        break;

                    // LOD R,
                    // SUB R,
                    // STO R,
                    case "-":
                        line++;
                        line++;
                        line++;
                        break;

                    // LOD R,
                    // MUL R,
                    // STO R,
                    case "*":
                        line++;
                        line++;
                        line++;
                        break;

                    // LOD R,
                    // DIV R,
                    // STO R,
                    case "/":
                        line++;
                        line++;
                        line++;
                        break;

                    // CMPGE T1,T2
                    case "==":
                        line++;
                        break;

                    // CMPGTE T1,T2
                    case ">=":
                        line++;
                        break;

                    // CMPGT T1,T2
                    case ">":
                        line++;
                        break;
                }
            }

            line = 0;
            foreach (InterCode a in Icode)
            {
                switch (a.op)
                {

                    // LBL 10
                    case "LBL":
                        break;

                    // JT 10
                    case "JT":
                        Console.WriteLine(line++ + ": JT " + SearchLBL(a.res));
                        break;

                    // JMP 10
                    case "JMP":
                        Console.WriteLine(line++ + ": JMP " + SearchLBL(a.res));
                        break;
                    // LOD R,
                    // STO R,
                    case "=":
                        Console.WriteLine(line++ + ": LOD R," + a.opr1);
                        Console.WriteLine(line++ + ": STO R," + a.res);
                        break;

                    // LOD R,
                    // ADD R,
                    // STO R,
                    case "+":
                        Console.WriteLine(line++ + ": LOD R," + a.opr1);
                        Console.WriteLine(line++ + ": ADD R," + a.opr2);
                        Console.WriteLine(line++ + ": STO R," + a.res);
                        break;

                    // LOD R,
                    // SUB R,
                    // STO R,
                    case "-":
                        Console.WriteLine(line++ + ": LOD R," + a.opr1);
                        Console.WriteLine(line++ + ": SUB R," + a.opr2);
                        Console.WriteLine(line++ + ": STO R," + a.res);
                        break;

                    // LOD R,
                    // MUL R,
                    // STO R,
                    case "*":
                        Console.WriteLine(line++ + ": LOD R," + a.opr1);
                        Console.WriteLine(line++ + ": MUL R," + a.opr2);
                        Console.WriteLine(line++ + ": STO R," + a.res);
                        break;

                    // LOD R,
                    // DIV R,
                    // STO R,
                    case "/":
                        Console.WriteLine(line++ + ": LOD R," + a.opr1);
                        Console.WriteLine(line++ + ": DIV R," + a.opr2);
                        Console.WriteLine(line++ + ": STO R," + a.res);
                        break;

                    // CMPGE T1,T2
                    case "==":
                        Console.WriteLine(line++ + ": CMPGE " + a.opr1 + ", " + a.opr2);
                        break;

                    // CMPGTE T1,T2
                    case ">=":
                        Console.WriteLine(line++ + ": CMPGTE " + a.opr1 + ", " + a.opr2);
                        break;

                    // CMPGT T1,T2
                    case ">":
                        Console.WriteLine(line++ + ": CMPGGT " + a.opr1 + ", " + a.opr2);
                        break;
                }
            }
        }


        string SearchLBL(string res)
        {
            foreach(LBLstruct a in LBLarray)
            {
                if (a.Number == res)
                    return a.Address;
            }
            return "??????";
        }







        void showIcode()
        {
            foreach (InterCode a in Icode)
                Console.WriteLine("{0} {1} {2} {3}", a.op, a.opr1 ,a.opr2 ,a.res); 
        }










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
    }
}










