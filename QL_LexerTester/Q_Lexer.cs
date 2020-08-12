// 19.11.2017 Lexer release 0.7
// Named processes and including files feature have been added
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_LexerTester
{
    // lexer_line
    public class lexeme_record
    {
        public Int32 line;
        public Int32 column;
        public Int32 start = 0;
        public Int32 lenght = 0;
        public char type;
        public string lexeme;
        public lexeme_record(char t, string l, Int32 ln, Int32 col, Int32 strt, Int32 lnght)
        { type = t; lexeme = l; line = ln; column = col; start = strt; lenght = lnght; }
        public lexeme_record(char t, string l) { type = t; lexeme = l; line = 0; column = 0; start = 0; lenght = 0; }
        public override string ToString()
        {
            return type.ToString() + "\t" + lexeme;
        }
    }

    public class error_record
    {
        public Int32 line;
        public Int32 column;
        public string message;
        public error_record(Int32 ln, Int32 col, string msg) { line = ln; column = col; message = msg; }
        public override string ToString() { return "error " + line.ToString() + ", " + column.ToString() + " - " + message; }
    }


    public class Q_Lexer
    {
        public Q_Lexer(char[] in_l, List<error_record> err_line)
        {
            in_line = in_l;
            error_line = err_line;
        }

        // in_line
        char[] in_line;

        List<lexeme_record> lexer_line = new List<lexeme_record>();

        List<error_record> error_line;

        public lexeme_record[] get_line() { finalize();  return lexer_line.ToArray(); }

        // positions
        Int32 prev_pos = 0, v_position, line = 1, column = 0;

        // Events' statuses
        string current_event = "";
        int comment_status = 0;
        int name_status = 0;
        int operator_status = 0;
        int constant_status = 0, constant_dec_separator = 0;
        int lexer_status = 0;

        //c_letter := (in_line == '_' | in_line <= 'a' & in_line <= 'z' | in_line <= 'A' & in_line <= 'Z')
        bool c_letter(Int32 pos)
        {
            return in_line[pos] == '_' | in_line[pos] >= 'a' & in_line[pos] <= 'z' | in_line[pos] >= 'A' & in_line[pos] <= 'Z';
        }

        //c_digit := (in_line >= '0' & in_line <= '9')
        bool c_digit(Int32 pos)
        {
            return in_line[pos] >= '0' & in_line[pos] <= '9';
        }

        //c_numeric_terminator := (in_line == 'p' | in_line == '%' | in_line == 'l' | in_line == 'c' | in_line == 'n' | in_line == 'i')
        bool c_numeric_terminator(Int32 pos)
        {
            return  in_line[pos] == 'p' | in_line[pos] == '%' | in_line[pos] == 'l' | in_line[pos] == 'c' | 
                    in_line[pos] == 'n' | in_line[pos] == 'i' |
                    // Time intervals
                    in_line[pos] == 'S' | in_line[pos] == 'M' | in_line[pos] == 'H' | in_line[pos] == 'D' | in_line[pos] == 'W'
                    ;
        }

        //c_constant := (c_digit | in_line == '"')
        bool c_constant(Int32 pos)
        {
            return c_digit(pos) | in_line[pos] == '"';
        }



        //c_operator_comment := 
        //(
        //  in_line == "?" | in_line == "|" | in_line == "&" | in_line == ";" | in_line[0..1] == ".." |
        //  in_line == "!" | in_line == "<" | in_line == ">" | in_line == "=" | in_line == "#" |
        //  in_line == "+" | in_line == "-" | in_line == "*" | (in_line == "/" & in_line[1] != "/" & in_line[1] != "*") |
        //  in_line == "(" | in_line == ")" | in_line == "[" | in_line == "]" | 
        //  in_line == "~" 
        //)
        bool c_operator_comment(Int32 pos)
        {
            return in_line[pos] == '?' | in_line[pos] == '|' | in_line[pos] == '&' | in_line[pos] == ';' |
                    in_line[pos] == '.' | in_line[pos] == '!' | in_line[pos] == '<' | in_line[pos] == '>' |
                    in_line[pos] == '=' | in_line[pos] == '#' | in_line[pos] == '+' | in_line[pos] == '-' |
                    in_line[pos] == '/' | in_line[pos] == '(' | in_line[pos] == ')' | in_line[pos] == '*' |
                    in_line[pos] == '[' | in_line[pos] == ']' | in_line[pos] == '~' | in_line[pos] == ':' |
                    in_line[pos] == ',' | in_line[pos] == '{' | in_line[pos] == '}';

        }

        //space :=
        //(
        //    ~ << !c_space
        //)
        bool c_space(Int32 pos)
        {
            return in_line[pos] == ' ' | in_line[pos] == '\n' | in_line[pos] == '\r' | in_line[pos] == '\t';
        }

        bool space(Int32 pos)
        {
            if (c_space(pos)) return true;
            else return false;
        }

        //put_name(name) :=
        //(
        //  lexer_line[0][0] = "n"; lexer_line[0][1] = name;
        //  lexer_line.next
        //)

        void put_lexeme(char c, Int32 start, Int32 count)
        {
            char[] cc = new char[count];
            int i = 0;
            while (i < count)
            {
                cc[i] = in_line[start + i];
                i++;
            }

            lexeme_record ll = new lexeme_record(c, new string(cc), line, column, start, count);
            lexer_line.Add(ll);
        }

        void put_constant(char c, char type, Int32 start, Int32 count)
        {
            char[] cc = new char[count + 1];
            cc[0] = type;
            int i = 1;
            while (i < count + 1)
            {
                cc[i] = in_line[start + i - 1];
                i++;
            }

            lexeme_record ll = new lexeme_record(c, new string(cc), line, column, start, count);
            lexer_line.Add(ll);
        }

        void put_error(string error_message)
        {
            error_record ll = new error_record(line, column, error_message);
            error_line.Add(ll);
        }

        //name :=
        //(
        //  v_position = in_line.pos;
        //  /*Variable*/
        //  ~;
        //  put_name(in_line[:v_position..pos-1]) << !c_letter & !c_digit & in_line != '_' & in_line != '.'
        //)

        bool name(Int32 pos)
        {
            if (name_status++ == 0) v_position = pos;

            if (!c_letter(pos) & !c_digit(pos) & in_line[pos] != '_' & in_line[pos] != '.')
            {
                put_lexeme('n', v_position, pos - v_position);
                name_status = 0;
                return false;
            }
            else return true;
        }

        // comparison < > <= >= == != #_ #^
        // logical neg !
        // combine & | ; ..
        // math + - / * ++ --
        // assign = += -= /= *=
        // brackets ( ) [ ] [:
        // skip ~
        // happened ?
        // named event :=
        // cause <<
        // break ##
        //c_operator :=
        //(
        //	        in_line[-1..0] == "==" | in_line[-1..0] == "<=" | in_line[-1..0] == ">=" | in_line[-1..0] == "!=" | 
        //          in_line[-1..0] == "#^" | in_line[-1..0] == "#_" | in_line[-1..0] == "##" | in_line[-1..0] == "<<" |
        //          in_line[-1..0] == "+=" | in_line[-1..0] == "-=" | in_line[-1..0] == "*=" | in_line[-1..0] == "-=" |
        //          in_line[-1..0] == "[:" | in_line[-1..0] == ".." | in_line[-1..0] == ":=" | in_line[-1..0] == "++" |
        //          in_line[-1..0] == "--"
        //)
        //e_operator_comment :=
        //(
        //  (~ << c_operator_comment; 
        //      (
        //          put_operator(in_line[-1..0]) << c_operator; 
        //          ~
        //      )
        //      |
        //      (
        //          put_operator(in_line[-1]) << c_operator_comment & !c_operator | c_constant | c_space | c_letter
        //      )
        //      |
        //      (
        //          ~ << in_line[0] == "/";
        //          ~ << in_line[0] == '\n'
        //      )
        //      |
        //      (
        //          ~ << in_line[0] == "*";
        //          ~ << in_line[-1..0] == "*/"
        //      )
        //      | (exception("Undefined operator") << !c_operator; ~)
        //  )
        //  | 
        //  (exception("Undefined operator") << !c_operator_comment; ~)
        //)
        bool c_operator(Int32 pos)
        {
            return
                in_line[pos - 1] == '=' & in_line[pos] == '=' | in_line[pos - 1] == '<' & in_line[pos] == '=' |
                in_line[pos - 1] == '>' & in_line[pos] == '=' | in_line[pos - 1] == '!' & in_line[pos] == '=' |
                in_line[pos - 1] == '#' & in_line[pos] == '_' | in_line[pos - 1] == '#' & in_line[pos] == '^' |
                in_line[pos - 1] == '+' & in_line[pos] == '=' | in_line[pos - 1] == '-' & in_line[pos] == '=' |
                in_line[pos - 1] == '*' & in_line[pos] == '=' | in_line[pos - 1] == '/' & in_line[pos] == '=' |
                in_line[pos - 1] == '[' & in_line[pos] == ':' | in_line[pos - 1] == ':' & in_line[pos] == '=' |
                in_line[pos - 1] == '.' & in_line[pos] == '.' | in_line[pos - 1] == '<' & in_line[pos] == '<' |
                in_line[pos - 1] == '#' & in_line[pos] == '#' | in_line[pos - 1] == '+' & in_line[pos] == '+' |
                in_line[pos - 1] == '-' & in_line[pos] == '-' | in_line[pos - 1] == '|' & in_line[pos] == '|' |
                in_line[pos - 1] == '&' & in_line[pos] == '&';
        }

        bool c_comment_start(Int32 pos)
        {
            return in_line[pos - 1] == '/' & (in_line[pos] == '/' | in_line[pos] == '*');
        }

        bool e_operator_comment(Int32 pos)
        {
            if (operator_status == 0)
            {
                if (c_operator_comment(pos))
                {
                    operator_status = 1;
                    return true;
                }
                else
                {
                    operator_status = 10;
                    return true;
                }
            }

            else if (operator_status == 1)
            {
                if (c_operator(pos))
                {
                    put_lexeme('o', pos - 1, 2);
                    operator_status = 2;
                    return true;
                }
                else if ((c_operator_comment(pos) & !c_operator(pos) | c_constant(pos) | c_letter(pos) | c_space(pos))
                    /*& in_line[pos] != '/' & in_line[pos] != '*'*/ & !c_comment_start(pos))
                {
                    put_lexeme('o', pos - 1, 1);
                    operator_status = 0;
                    return false;

                }
                else if (in_line[pos-1] == '/' & in_line[pos] == '/')
                {
                    operator_status = 5;
                    return true;
                }
                else if (in_line[pos-1] == '/' & in_line[pos] == '*')
                {
                    operator_status = 7;
                    return true;
                }
                else
                {
                    put_error("lexer: undefined operator");
                    operator_status = 0;
                    return false;
                }
            }

            else if (operator_status == 2)
            {
                operator_status = 0;
                return false;
            }

            else if (operator_status == 5)
            {
                if (in_line[pos - 1] == '\n')
                {
                    operator_status = 0;
                    return false;
                }
                else return true;
            }

            else if (operator_status == 7)
            {
                if (in_line[pos - 2] == '*' & in_line[pos - 1] == '/')
                {
                    operator_status = 0;
                    return false;
                }
                else return true;
            }

            else /*if (operator_status == 3 | operator_status == 5)*/
            {
                put_error("lexer: undefined operator");
                operator_status = 0;
                return false;
            }
        }


        // constant := numeric | string | time
        // numeric := digit; (..[..]digit) | (); (.; ..[..]digit) | ()
        // string := "; (..[..]any_symbol) | (); "
        // time := digit; digit; :; digit; digit; (_; digit; digit; .; digit; digit; .; digit; digit) | ()
        //
        //numeric(v_position) :=
        //(
        //	(put_constant(in_line + in_line[:v_position..pos-1]) << c_numeric_terminator; ~)
        //	|
        //	exception("Undefined symbol; a digit or decimal point or type char expected") 
        //      << !c_digit & !'.' & !c_terminator | '.' & ?('.')
        //)
        //
        //time(v_position)
        //(
        //	~ << ':' | exception("Undefined symbol; a time separator expected") << !':';
        //	~ << c_digit | exception("Undefined symbol; a digit expected") << !c_digit;
        //	~ << c_digit | exception("Undefined symbol; a digit expected") << !c_digit;
        //	(~ << '_';)
        //	|
        //	exception("Undefined symbol; a digit expected") << !'_' & !c_space & !c_operator_comment
        //)
        //
        //constant :=
        //(
        //	v_position = pos << c_digit; // [0]
        //	~; 
        //	(numeric(v_position) << c_terminator; ~) 
        //	| 
        //	(<< c_digit; ~)
        //	| 
        //	numeric(v_position) << '.'
        //	|
        //	exception("Undefined symbol; a digit or decimal point or type char expected") << !c_digit & !'.' & !c_terminator;// [1]
        //	
        //	numeric(v_position) << c_terminator
        //	| 
        //	numeric(v_position) << '.'
        //	|
        //	time(v_position) << ':';
        //	|
        //	exception("Undefined symbol; a digit or decimal point or time separator or type char expected") << !c_digit & !'.' & !c_terminator !':'// [2]
        //)
        //| 
        //(
        //	<< '"'; 
        //	~;
        //	v_position = pos;
        //	put_constant("s" + in_line[:v_position+1..pos-1]) << '"';
        //	~
        //)
        bool constant(Int32 pos)
        {
            if (in_line[pos] == '.') constant_dec_separator++;

            // Accumulating number or time or going to accumulating string
            if (constant_status == 0)
            {
                if (c_digit(pos))
                {
                    constant_status = 1;
                    constant_dec_separator = 0;
                    return true;
                }
                else if (in_line[pos] == '"')
                {
                    constant_status = 100;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating number or time
            else if (constant_status == 1)
            {
                if (c_numeric_terminator(pos))
                {
                    put_constant('c', in_line[pos], pos - constant_status, constant_status);
                    constant_status = 1000;
                    return true;
                }
                else if (c_digit(pos) | in_line[pos] == '.' & constant_dec_separator < 2)
                {
                    constant_status = 2;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit or decimal point or type char expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating number or going to accumulating time
            else if (constant_status == 2)
            {
                if (c_numeric_terminator(pos))
                {
                    put_constant('c', in_line[pos], pos - constant_status, constant_status);
                    constant_status = 1000;
                    return true;
                }
                else if (c_digit(pos) | in_line[pos] == '.' & constant_dec_separator < 2)
                {
                    constant_status = 3;
                    return true;
                }
                else if (in_line[pos] == ':')
                {
                    constant_status = 10003;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit or decimal point or type char or time separator expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating number
            else if (constant_status > 2 & constant_status < 100)
            {
                if (c_numeric_terminator(pos))
                {
                    put_constant('c', in_line[pos], pos - constant_status, constant_status);
                    constant_status = 1000;
                    return true;
                }
                else if (c_digit(pos) | in_line[pos] == '.' & constant_dec_separator < 2)
                {
                    constant_status++;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit or decimal point or type char expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Finalizing number or string
            else if (constant_status == 1000 | constant_status == 20000)
            {
                constant_status = 0;
                return false;
            }

            // Accumulating string
            else if (constant_status >= 100 & constant_status < 1000)
            {
                if (in_line[pos] == '"')
                {
                    put_constant('c', 's', pos - constant_status + 100, constant_status - 100);
                    constant_status = 20000;
                    return true;
                }
                else
                {
                    constant_status++;
                    return true;
                }
            }

            // Accumulating time
            else if (constant_status == 10003)
            {
                if (c_digit(pos))
                {
                    constant_status = 10004;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10004)
            {
                if (c_digit(pos))
                {
                    constant_status = 10005;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10005)
            {
                if (in_line[pos] == '_')
                {
                    constant_status = 10006;
                    return true;
                }
                else if (c_space(pos) | c_operator_comment(pos))
                {
                    if (in_line[pos - 5] > '2' | in_line[pos - 4] > '3' & in_line[pos - 5] == '2' | in_line[pos - 2] > '5')
                    {
                        put_error("Undefined symbol; corrupted time format");
                        constant_status = 0;
                        return false;
                    }
                    else
                    {
                        put_constant('c', 'x', pos - 5, 5);
                        constant_status = 0;
                        return false;
                    }
                }
                else
                {
                    put_error("Undefined symbol; a time-date separator, operator or space expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10006 | constant_status == 10007)
            {
                if (c_digit(pos))
                {
                    constant_status++;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10008)
            {
                if (in_line[pos] == '.')
                {
                    constant_status++;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a date separator expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10009 | constant_status == 10010)
            {
                if (c_digit(pos))
                {
                    constant_status++;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10011)
            {
                if (in_line[pos] == '.')
                {
                    constant_status++;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a date separator expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Accumulating time
            else if (constant_status == 10012 | constant_status == 10013)
            {
                if (c_digit(pos))
                {
                    constant_status++;
                    return true;
                }
                else
                {
                    put_error("Undefined symbol; a digit expected");
                    constant_status = 0;
                    return false;
                }
            }

            // Finalizing time
            else if (constant_status == 10014)
            {
                if (in_line[pos - 14] > '2' | in_line[pos - 13] > '3' & in_line[pos - 14] == '2' | in_line[pos - 11] > '5' |
                    in_line[pos - 8] > '3' | in_line[pos - 7] > '1' & in_line[pos - 8] == '3' | in_line[pos - 5] > '1' | in_line[pos - 4] > '2' & in_line[pos - 5] == '1')
                {
                    put_error("Undefined symbol; corrupted time format");
                    constant_status = 0;
                    return false;
                }
                else
                {
                    put_constant('c', 'x', pos - 14, 14);
                    constant_status = 0;
                    return false;
                }
            }

            else
            {
                put_error("Undefined symbol");
                constant_status = 0;
                return false;
            }

        }

        // lexer := ((constant << c_constant) | (name << c_letter) | (space << c_spase) | (e_operator_operator << !c_constant & !c_letter & !c_space))
        public bool lexer(Int32 pos)
        {
            if (prev_pos != pos)
            {
                prev_pos = pos;
                column++;
                if (in_line[pos] == '\n')
                {
                    column = 1; line++;
                }

            }
            
            if (lexer_status++ == 0)
            {
                if (c_constant(pos)) current_event = "constant";
                if (c_letter(pos)) current_event = "name";
                if (c_space(pos)) current_event = "space";
                //if (c_comment(pos)) current_event = "comment";
                if (!c_constant(pos) & !c_letter(pos) & !c_space(pos)) current_event = "operator";
            }

            if (current_event == "name")
            {
                if (name(pos) == false) { lexer_status = 0; current_event = ""; lexer(pos); }
            }
            else if (current_event == "constant")
            {
                if (constant(pos) == false) { lexer_status = 0; current_event = ""; lexer(pos); }
            }
            else if (current_event == "space")
            {
                if (space(pos) == false) { lexer_status = 0; current_event = ""; lexer(pos); }
            }
            else if (current_event == "operator")
            {
                if (e_operator_comment(pos) == false) { lexer_status = 0; current_event = ""; lexer(pos); }
            }
            else
            {
                lexer_status = 0; current_event = "";
            }

            return true;
        }

        public void finalize()
        {
            lexeme_record ll = new lexeme_record('o', "eol", line, column, prev_pos, 1);
            lexer_line.Add(ll);

        }

    }
}
