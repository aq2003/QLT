// 4.11.2015 Parcer release 0.6
// Indexing has been added
// 19.11.2017 Parcer release 0.7
// Named processes and including files feature have been added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_LexerTester
{
    // parcer_line
    public class parcer_record
    {
        public static int arg_count = 10;
        
        public string name;
        public string code;
        public string condition;
        public string next;
	    public string[] arg;

	    public parcer_record(string c) 
	    {
		    code = c;
            condition = "e0";
            next = "e0";
            arg = new string[arg_count];
            int i = arg_count;
            while (i-- > 0) arg[i] = "n0";
	    }

        public string ToString(int arg_number)
        {
            string res = name + "\t" + code + "\t" + condition + "\t" + next;

            int i = 0;
            while (i < arg_number) res += "\t" + arg[i++];
            res += "\n";

            return res;
        }
    }
	
    class Q_Parcer
    {
        protected class ProcessRecord
        {
            public string ProcessName;
            public string StartPoint;
            public List<string> VariablesTable;

            public ProcessRecord(string proc_name, string start_point, List<string> var_table)
            {
                ProcessName = proc_name;
                StartPoint = start_point;
                VariablesTable = var_table;
            }
        }

        int currentPos;
        public int CurrentPos { get { return currentPos; } }

        string ProcessName;

        int CurrentAddress = 0;

        List<string> variables_table;
        Dictionary<string, ProcessRecord> ProcessTable = new Dictionary<string, ProcessRecord>();

        lexeme_record[] lexer_line;

        List<parcer_record> parcer_line = new List<parcer_record>();
        public parcer_record[] get_line() { return parcer_line.ToArray(); }

        List<error_record> error_line;

        Stack<string> parcer_status;
        Stack<lexeme_record> stack;
        Stack<int> expression_terminator;
        Stack<parcer_record> event_stack;
        Stack<parcer_record> command_stack;

        static int rat_const_name = 1000;
        static int rat_pre_operator = 120;
        static int rat_assign_operator = 110;
        static int rat_mult_operator = 100;
        static int rat_add_operator = 99;
        static int rat_compare_operator = 98;
        static int rat_logical_and_operator = 97;
        static int rat_logical_or_operator = 96;
        static int rat_comma = 23;
        static int rat_closing_bracket = 20;
        static int rat_opening_bracket = 20;
        static int rat_closing_sq_bracket = 19;
        static int rat_opening_sq_bracket = 19;
        static int rat_cause_operator = 17;
        static int rat_dots = 18;
        static int rat_semicolon = 16;
        static int rat_closing_cv_bracket = 15;
        static int rat_opening_cv_bracket = 15;
        static int rat_event_and_operator = 14;
        static int rat_event_or_operator = 13;
        static int rat_eol = 10; 

        // A special flag to watch comparisons have only two args
        int comparison_times;

        public Q_Parcer(lexeme_record[] in_l, List<error_record> err_line, string process_name, List<string> var_table = null)
        {
            lexer_line = in_l;
            error_line = err_line;

            stack = new Stack<lexeme_record>();
            parcer_status = new Stack<string>();
            expression_terminator = new Stack<int>();
            event_stack = new Stack<parcer_record>();
            command_stack = new Stack<parcer_record>();

            ProcessName = process_name;

            parcer_status.Push("event_list");
            expression_terminator.Push(rat_eol);
            event_stack.Push(new parcer_record("event_list_start_mark"));

            variables_table = (var_table == null) ? new List<string>() : var_table;
        }

        bool c_assign(Int32 pos) { return c_assign(lexer_line[pos]); }
        bool c_assign(lexeme_record rec)
        {
            return rec.type == 'o' & (rec.lexeme == "=" | rec.lexeme == "+=" | rec.lexeme == "-=" | rec.lexeme == "*=" | rec.lexeme == "/=");
        }

        bool c_add(Int32 pos) { return c_add(lexer_line[pos]); }
        bool c_add(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "+" | rec.lexeme == "-"); }

        bool c_mult(Int32 pos) { return c_mult(lexer_line[pos]); }
        bool c_mult(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "*" | rec.lexeme == "/"); }

        bool c_compare(Int32 pos) { return c_compare(lexer_line[pos]); }
        bool c_compare(lexeme_record rec)
        {
            return rec.type == 'o' & (rec.lexeme == ">" | rec.lexeme == "<" | rec.lexeme == ">=" | rec.lexeme == "<=" |
                  rec.lexeme == "==" | rec.lexeme == "!=" | rec.lexeme == "#^" | rec.lexeme == "#_");
        }

        bool c_logical_or(Int32 pos) { return c_logical_or(lexer_line[pos]); }
        bool c_logical_or(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "|"); }

        bool c_logical_and(Int32 pos) { return c_logical_and(lexer_line[pos]); }
        bool c_logical_and(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "&"); }

        bool c_name(Int32 pos) { return c_name(lexer_line[pos]); }
        bool c_name(lexeme_record rec) { return rec.type == 'n'; }

        bool c_const(Int32 pos) { return c_const(lexer_line[pos]); }
        bool c_const(lexeme_record rec) { return rec.type == 'c'; }

        bool c_name_const(Int32 pos) { return c_name_const(lexer_line[pos]); }
        bool c_name_const(lexeme_record rec) { return c_name(rec) | c_const(rec); }

        bool c_end_exp(Int32 pos) { return c_end_exp(lexer_line[pos]); }
        bool c_end_exp(lexeme_record rec)
        {
            return
                rec.type == 'o' &
                    (rec.lexeme == ")" | rec.lexeme == ";" |
                     rec.lexeme == "||" | rec.lexeme == "&&" |
                     rec.lexeme == "," | rec.lexeme == "eol");
        }

        bool c_call(Int32 pos) { return c_call(lexer_line[pos]); }
        bool c_call(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "("; }

        bool c_NamedProcess(Int32 pos) { return c_NamedProcess(lexer_line[pos]); }
        bool c_NamedProcess(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == ":="; }

        bool c_open_bracket(Int32 pos) { return c_open_bracket(lexer_line[pos]); }
        bool c_open_bracket(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "("; }

        bool c_close_bracket(Int32 pos) { return c_close_bracket(lexer_line[pos]); }
        bool c_close_bracket(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == ")"; }

        bool c_comma(Int32 pos) { return c_comma(lexer_line[pos]); }
        bool c_comma(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == ","; }

        bool c_eol(Int32 pos) { return c_eol(lexer_line[pos]); }
        bool c_eol(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "eol"; }

        bool c_pre_op(Int32 pos) { return c_pre_op(lexer_line[pos]); }
        bool c_pre_op(lexeme_record rec)
        {
            return rec.type == 'o' &
                (rec.lexeme == "-" | rec.lexeme == "!" | rec.lexeme == "+" |
                 rec.lexeme == "--" | rec.lexeme == "++");
        }

        bool c_post_op(Int32 pos) { return c_post_op(lexer_line[pos]); }
        bool c_post_op(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "--" | rec.lexeme == "++"); }

        bool c_cause(Int32 pos) { return c_cause(lexer_line[pos]); }
        bool c_cause(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "<<"); }

        bool c_tild(Int32 pos) { return c_tild(lexer_line[pos]); }
        bool c_tild(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "~"; }

        bool c_dots(Int32 pos) { return c_dots(lexer_line[pos]); }
        bool c_dots(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == ".."; }

        bool c_open_sq_bracket(Int32 pos) { return c_open_sq_bracket(lexer_line[pos]); }
        bool c_open_sq_bracket(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "["; }

        bool c_close_sq_bracket(Int32 pos) { return c_close_sq_bracket(lexer_line[pos]); }
        bool c_close_sq_bracket(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "]"; }

        bool c_semicolon(Int32 pos) { return c_semicolon(lexer_line[pos]); }
        bool c_semicolon(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == ";"); }

        bool c_open_cv_bracket(Int32 pos) { return c_open_cv_bracket(lexer_line[pos]); }
        bool c_open_cv_bracket(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "{"; }

        bool c_close_cv_bracket(Int32 pos) { return c_close_cv_bracket(lexer_line[pos]); }
        bool c_close_cv_bracket(lexeme_record rec) { return rec.type == 'o' & rec.lexeme == "}"; }

        bool c_event_and(Int32 pos) { return c_event_and(lexer_line[pos]); }
        bool c_event_and(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "&&"); }

        bool c_event_or(Int32 pos) { return c_event_or(lexer_line[pos]); }
        bool c_event_or(lexeme_record rec) { return rec.type == 'o' & (rec.lexeme == "||"); }

        int c_operator_rating(Int32 pos) { return c_operator_rating(lexer_line[pos]); }
        int c_operator_rating(lexeme_record rec)
        {
            if (c_pre_op(rec) & parcer_status.Peek() == "begin_expression") return rat_pre_operator;
            else if (c_name_const(rec) | c_tild(rec)) return rat_const_name;
            else if (c_assign(rec)) return rat_assign_operator;
            else if (c_mult(rec)) return rat_mult_operator;
            else if (c_add(rec)) return rat_add_operator;
            else if (c_compare(rec)) return rat_compare_operator;
            else if (c_logical_and(rec)) return rat_logical_and_operator;
            else if (c_logical_or(rec)) return rat_logical_or_operator;
            else if (c_comma(rec)) return rat_comma;
            else if (c_close_bracket(rec)) return rat_closing_bracket;
            else if (c_open_bracket(rec)) return rat_opening_bracket;
            else if (c_close_sq_bracket(rec)) return rat_closing_sq_bracket;
            else if (c_open_sq_bracket(rec)) return rat_opening_sq_bracket;
            else if (c_cause(rec)) return rat_cause_operator;
            else if (c_dots(rec)) return rat_dots;
            else if (c_semicolon(rec)) return rat_semicolon;
            else if (c_close_cv_bracket(rec)) return rat_closing_cv_bracket;
            else if (c_open_cv_bracket(rec)) return rat_opening_cv_bracket;
            else if (c_event_and(rec)) return rat_event_and_operator;
            else if (c_event_or(rec)) return rat_event_or_operator;
            else if (c_eol(rec)) return rat_eol;
            else return 0;
        }

        void put_error(lexeme_record rec, string error_message)
        {
            error_line.Add(new error_record(rec.line, rec.column, error_message));
        }

        string GetAddress()
        {
            return "e_" + ProcessName + ((ProcessName != "")? "_" : "") + ((parcer_line.Count() - CurrentAddress) * 100 + command_stack.Count).ToString();
        }

        string put_parcer_line_no_arg(string operat)
        {
            parcer_record parcer_r = new parcer_record(operat);

            parcer_r.name = GetAddress();

            //parcer_line.Add(parcer_r);
            command_stack.Push(parcer_r);

            return parcer_r.name;
        }

        string put_parcer_line_1_arg(string operat, string arg)
        {
            parcer_record parcer_r = new parcer_record(operat);

            parcer_r.arg[0] = arg;
            parcer_r.name = GetAddress();

            //parcer_line.Add(parcer_r);
            command_stack.Push(parcer_r);

            return parcer_r.name;
        }

        string put_parcer_line_2_arg(string operat, string arg1, string arg2)
        {
            parcer_record parcer_r = new parcer_record(operat);

            parcer_r.arg[0] = arg1;
            parcer_r.arg[1] = arg2;
            parcer_r.name = GetAddress();

            command_stack.Push(parcer_r);

            return parcer_r.name;
        }

        string put_parcer_line_call(string name, string[] arg, int count)
        {
            // There are 2 ways to call a function: 
            //  1) calling an embedded command and
            //  2) calling a named process 
            // To recognize the way it needs to check ProcessTable; if name is presented there it means the way 2; else it means the way 1
            // To call a named process AQL uses a command ":=" (call)
            // IMPORTANT! Named process must be defined before first use otherwise it causes an error

            parcer_record parcer_r = new parcer_record(name);

            // Selecting the way
            if (!ProcessTable.ContainsKey(((ProcessName.Length > 0) ? ProcessName + "_" : "") + name))
            {
                // The way 1 - embedded command
                if (count > parcer_record.arg_count) parcer_r.arg = new string[count];
                int i = count;
                while (i-- > 0)
                {
                    parcer_r.arg[i] = arg[i];
                }
            }
            else
            {
                // The way 2 - named process
                name = ((ProcessName.Length > 0) ? ProcessName + "_" : "") + name;
                /*if (count + 1 > parcer_record.arg_count)*/ parcer_r.arg = new string[2*count + 1];

                parcer_r.code = ":=";
                parcer_r.arg[0] = ProcessTable[name].StartPoint;

                int i = count + 1;
                while (i-- > 1)
                {
                    parcer_r.arg[2*i] = arg[i - 1];
                    parcer_r.arg[2*i - 1] = "s" + ProcessTable[name].VariablesTable[i - 1];
                }
            }

            parcer_r.name = GetAddress()/*"e_" + (parcer_line.Count() * 100 + command_stack.Count).ToString()*/;

            command_stack.Push(parcer_r);

            return parcer_r.name;
        }

        string roll_up_1_arg()
        {
            string arg = stack.Pop().lexeme;
            string op = stack.Pop().lexeme;

            stack.Push(new lexeme_record('e', put_parcer_line_1_arg((op == "-")? "neg" : op, arg)));

            parcer_status.Pop(); 

            return parcer_status.Peek();
        }

        string roll_up_2_args()
        {
            string arg2 = stack.Pop().lexeme;
            string op = stack.Pop().lexeme;
            string arg1 = stack.Pop().lexeme;

            stack.Push(new lexeme_record('e', put_parcer_line_2_arg(op, arg1, arg2)));

            parcer_status.Pop(); parcer_status.Pop();

            return parcer_status.Peek();
        }

        string roll_up_call()
        {
            Int32 count = Convert.ToInt32(stack.Pop().lexeme);
            int i = count;
            string[] arg = new string[count];

            while (i-- > 0)
            {
                arg[i] = stack.Pop().lexeme;
            }

            //if (!ProcessTable.ContainsKey(stack.Peek().lexeme))
            //{
                stack.Push(new lexeme_record('e', put_parcer_line_call(stack.Pop().lexeme, arg, count)));
            //}

            parcer_status.Pop();

            return parcer_status.Peek();
        }

        ProcessRecord roll_up_ProcessDefinition()
        {
            Int32 count = Convert.ToInt32(stack.Pop().lexeme);
            int i = count;
            string[] arg = new string[count];

            List<string> var_table = new List<string>();

            while (i-- > 0)
                arg[i] = stack.Pop().lexeme;

            var_table.AddRange(arg);

            string start_point = "e_" + ((ProcessName != "") ? ProcessName + "_" : "") + stack.Peek().lexeme + "_0";
            string process_name = ((ProcessName != "") ? ProcessName + "_" : "") + stack.Peek().lexeme;

            parcer_status.Pop();

            return new ProcessRecord(process_name, start_point, var_table);
        }

        // Variables are being managed here
        void roll_up_event()
        {
            if (command_stack.Count > 0)
            {
                // Push event in event stack
                if (command_stack.Peek().code == "=" | command_stack.Peek().code == "+=" | command_stack.Peek().code == "-=" |
                    command_stack.Peek().code == "*=" | command_stack.Peek().code == "/=" /*| command_stack.Peek().code == ".."*/)
                {
                    string var_name = /*(command_stack.Peek().code == "..") ? "v_" + command_stack.Peek().name :*/ 
                        /*"v_" + ProcessName + ((ProcessName != "")? "_" : "") +*/ command_stack.Peek().arg[0];
                    if (!variables_table.Contains(var_name))
                    {
                        variables_table.Add(var_name);
                        //parcer_record rr = new parcer_record("new");
                        //rr.name = GetAddress();
                        //rr.arg[0] = var_name;
                        //event_stack.Push(rr);
                        //parcer_line.Add(rr);
                    }
                }
                event_stack.Push(command_stack.Peek());

                // Putting event code
                while (command_stack.Count > 0)
                    parcer_line.Add(command_stack.Pop());

                stack.Pop();
            }
        }

        void roll_up_condition()
        {
            if (command_stack.Count != 0)
            {
                // Putting condition name
                int i = parcer_line.Count() - 1;
                while (parcer_line[i].name != event_stack.Peek().name) i--;

                //if ((event_stack.Peek().code == "=" | event_stack.Peek().code == "+=" | event_stack.Peek().code == "-=" |
                //    event_stack.Peek().code == "*=" | event_stack.Peek().code == "/=" /*| event_stack.Peek().code == ".."*/) & i > 0)
                //{
                //    if (parcer_line[i - 1].code == "new")
                //        parcer_line[i - 1].condition = command_stack.Peek().name;
                //}
                //else
                    parcer_line[i].condition = command_stack.Peek().name;

                // Putting condition code
                while (command_stack.Count > 0)
                    parcer_line.Add(command_stack.Pop());

                stack.Pop();
            }
        }

        void roll_up_cycle_condition()
        {
            if (command_stack.Count != 0)
            {
                // Putting condition name
                int i = parcer_line.Count() - 1;
                while (parcer_line[i].name != event_stack.Peek().name) i--;
                parcer_line[i].arg[0] = command_stack.Peek().name;

                // Putting condition code
                while (command_stack.Count > 0)
                    parcer_line.Add(command_stack.Pop());
            }
        }

        void roll_up_cycle_body()
        {
            parcer_record body = event_stack.Pop();
            parcer_record cycle = event_stack.Pop();
        
            cycle.arg[1] = body.name;

            event_stack.Push(cycle);
        }
        // rat: "pop_mark" - to pop mark, "not_pop_mark" - to not pop mark
        bool roll_up_event_list(string rat)
        {
            if (event_stack.Count > 1)
            {
                parcer_record next = event_stack.Pop();
                parcer_record prev = event_stack.Peek();

                while (event_stack.Peek().code != "event_list_start_mark")
                {
                    int i = parcer_line.Count() - 1;
                    while (parcer_line[i].name != prev.name) i--;

                    parcer_line[i].next = next.name;

                    next = event_stack.Pop();
                    prev = event_stack.Peek();

                    if (event_stack.Count == 1 & event_stack.Peek().code != "event_list_start_mark")
                        return false;
                }

                if (rat == "pop_mark") event_stack.Pop();
                event_stack.Push(next);
            }

            return true;
        }

        void roll_up_event_or()
        {
            string body = event_stack.Pop().name;
            put_parcer_line_1_arg("||", "e0");
            parcer_record rec = command_stack.Pop();
            rec.arg[1] = body;
            parcer_line.Add(rec);
            event_stack.Push(rec);
        }

        void roll_up_event_and()
        {
            string body = event_stack.Pop().name;
            put_parcer_line_1_arg("&&", "e0");
            parcer_record rec = command_stack.Pop();
            rec.arg[1] = body;
            parcer_line.Add(rec);
            event_stack.Push(rec);
        }

        void roll_up_event_or_list()
        {
            if (event_stack.Count > 1)
            {
                parcer_record next = event_stack.Pop();
                parcer_record prev = event_stack.Peek();

                while (event_stack.Peek().code != "event_or_list_start_mark")
                {
                    int i = parcer_line.Count() - 1;
                    while (parcer_line[i].name != prev.name) i--;

                    parcer_line[i].arg[0] = next.name;

                    next = event_stack.Pop();
                    prev = event_stack.Peek();
                }

                event_stack.Pop();
                event_stack.Push(next);
            }
        }

        void roll_up_event_and_list()
        {
            if (event_stack.Count > 1)
            {
                parcer_record next = event_stack.Pop();
                parcer_record prev = event_stack.Peek();

                while (event_stack.Peek().code != "event_and_list_start_mark")
                {
                    int i = parcer_line.Count() - 1;
                    while (parcer_line[i].name != prev.name) i--;

                    parcer_line[i].arg[0] = next.name;

                    next = event_stack.Pop();
                    prev = event_stack.Peek();
                }

                event_stack.Pop();
                event_stack.Push(next);
            }
        }

        void push_next(string arg, Int32 pos)
        {
            stack.Push(lexer_line[pos]);
            parcer_status.Push(arg);
        }

        void exp_double_operator(int my_rating, int curr_rating, string my_status, string up_status, Int32 pos)
        {
            // Higher
            if (curr_rating > my_rating)
            {
                push_next(up_status, pos);
            }

            // This
            else if (curr_rating == my_rating)
            {
                roll_up_2_args();
                push_next(my_status, pos);
            }

            // Lower needs or End need to roll up the previous
            else if (curr_rating < my_rating /*& curr_rating >= c_operator_rating(expression_terminator.Peek())*/)
            {
                roll_up_2_args();
                expression(pos);
            }

            else put_error(lexer_line[pos], "Parcer: " + my_status + " Undefined symbol in expression; a math operator or end of expression expected");

        }

        void new_expression(int terminator, string next_step)
        {
            parcer_status.Push(next_step); parcer_status.Push("begin_expression");
            expression_terminator.Push(terminator);
        }

        public void expression(Int32 pos)
        {
            if (parcer_status.Peek() == "event_or_list")
            {
                if (c_event_or(pos))
                {
                    // Next entry event or
                    roll_up_event_or();
                    event_stack.Push(new parcer_record("event_list_start_mark"));
                    parcer_status.Push("event_list");
                }

                else if (c_operator_rating(pos) < rat_event_or_operator & c_operator_rating(pos) >= rat_eol |
                            c_close_cv_bracket(pos) & expression_terminator.Peek() == rat_event_or_operator)
                {
                    roll_up_event_or();
                    roll_up_event_or_list();
                    expression_terminator.Pop();
                    if (parcer_status.Count > 1)
                    {
                        parcer_status.Pop();
                        expression(pos);
                    }
                }

            }

            else if (parcer_status.Peek() == "event_and_list")
            {
                if (c_event_and(pos))
                {
                    // Next entry event and
                    roll_up_event_and();
                    event_stack.Push(new parcer_record("event_list_start_mark"));
                    parcer_status.Push("event_list");
                }

                else if (c_operator_rating(pos) < rat_event_and_operator & c_operator_rating(pos) >= rat_eol |
                            c_close_cv_bracket(pos) & expression_terminator.Peek() == rat_event_and_operator)
                {
                    roll_up_event_and();
                    roll_up_event_and_list();
                    expression_terminator.Pop();
                    if (parcer_status.Count > 1)
                    {
                        parcer_status.Pop();
                        expression(pos);
                    }
                }

            }

            else if (parcer_status.Peek() == "event_list")
            {
                if (c_open_cv_bracket(pos) | c_operator_rating(pos) > rat_semicolon)
                {
                    parcer_status.Push("event");
                    expression(pos);
                }

                else if (c_semicolon(pos))
                {
                    parcer_status.Push("event");
                }

                else if (c_close_cv_bracket(pos) & expression_terminator.Peek() == rat_closing_cv_bracket)
                {
                    parcer_status.Pop();
                    expression_terminator.Pop();
                    if (roll_up_event_list("pop_mark") != true)
                        put_error(lexer_line[pos], "Parcer : rolling up event list has failed");
                }

                else if (c_operator_rating(pos) < rat_semicolon & 
                            /*expression_terminator.Peek() != rat_closing_cv_bracket &*/ 
                            c_operator_rating(pos) >= rat_eol)
                {
                    if (c_event_or(pos))
                    {
                        if (expression_terminator.Peek() != rat_event_or_operator)
                        // First entry to event or
                        {
                            if (roll_up_event_list("not_pop_mark") != true)
                                put_error(lexer_line[pos], "Parcer : rolling up event list has failed");
                            parcer_record rec = event_stack.Pop();
                            event_stack.Push(new parcer_record("event_or_list_start_mark"));
                            event_stack.Push(rec);
                            parcer_status.Push("event_or_list");
                            expression_terminator.Push(rat_event_or_operator);
                        }
                        else
                        // Next entry to event or
                        {
                            if (roll_up_event_list("pop_mark") != true)
                                put_error(lexer_line[pos], "Parcer : rolling up event list has failed");
                            parcer_status.Pop();
                        }

                        expression(pos);
                    }
                    else if (c_event_and(pos))
                    {
                        if (expression_terminator.Peek() != rat_event_and_operator)
                        // First entry to event and
                        {
                            if (roll_up_event_list("not_pop_mark") != true)
                                put_error(lexer_line[pos], "Parcer : rolling up event list has failed");
                            parcer_record rec = event_stack.Pop();
                            event_stack.Push(new parcer_record("event_and_list_start_mark"));
                            event_stack.Push(rec);
                            parcer_status.Push("event_and_list");
                            expression_terminator.Push(rat_event_and_operator);
                        }
                        else
                        // Next entry to event and
                        {
                            if (roll_up_event_list("pop_mark") != true)
                                put_error(lexer_line[pos], "Parcer : rolling up event list has failed");
                            parcer_status.Pop();
                        }

                        expression(pos);
                    }
                    else
                    {
                        if (roll_up_event_list("pop_mark") != true)
                            put_error(lexer_line[pos], "Parcer : rolling up event list has failed");
                        if (parcer_status.Count > 1)
                        {
                            parcer_status.Pop();
                            expression(pos);
                        }
                    }
                    //else
                    //{
                    //    parcer_record list = event_stack.Pop();
                    //    if (event_stack.Peek().code == "||")
                    //    {
                    //        event_stack.Push(list);
                    //        roll_up_event_or();
                    //        roll_up_event_or_list();
                    //    }
                    //}
                }

                else put_error(lexer_line[pos], "Parcer : undefined symbol in event list");
            }

            else if (parcer_status.Peek() == "event")
            {
                if (c_open_cv_bracket(pos))
                {
                    parcer_status.Pop();
                    parcer_status.Push("event_condition");
                    parcer_status.Push("event_list");
                    expression_terminator.Push(rat_closing_cv_bracket);
                    event_stack.Push(new parcer_record("event_list_start_mark"));
                }

                else if (c_dots(pos))
                {
                    parcer_status.Pop();
                    parcer_status.Push("event_next");
                    parcer_status.Push("cycle_condition");
                    put_parcer_line_2_arg("..", "e0", "e0");
                    stack.Push(lexer_line[pos]);
                    roll_up_event();
                }

                else if (c_tild(pos))
                {
                    parcer_status.Pop();
                    put_parcer_line_no_arg("~");
                    stack.Push(lexer_line[pos]);
                    new_expression(rat_cause_operator, "event_condition");
                    //expression(pos);
                }

                else if (c_operator_rating(pos) > expression_terminator.Peek())
                {
                    parcer_status.Pop();
                    new_expression(rat_cause_operator, "event_condition");
                    expression(pos);
                }

                else if (c_operator_rating(pos) <= expression_terminator.Peek() & 
                            expression_terminator.Peek() != rat_closing_bracket & 
                            c_operator_rating(pos) >= rat_eol)
                {
                    parcer_status.Pop();
                    expression(pos);
                }

                else if (c_close_cv_bracket(pos) & expression_terminator.Peek() == rat_closing_cv_bracket)
                {
                    parcer_status.Pop();
                    expression(pos);
                }

                else put_error(lexer_line[pos], "Parcer : undefined symbol in event");
            }

            else if (parcer_status.Peek() == "event_condition")
            {                
                if (c_cause(pos))
                {
                    // Get condition
                    roll_up_event();
                    parcer_status.Pop();
                    new_expression(rat_semicolon, "event_next");
                }

                else
                {
                    roll_up_event();
                    parcer_status.Pop();
                    parcer_status.Push("event_next");
                    if (expression_terminator.Peek() == rat_cause_operator)  expression_terminator.Pop();
                    expression(pos);
                }
            }

            else if (parcer_status.Peek() == "event_next")
            {
                if (c_operator_rating(pos) == rat_semicolon)
                {
                    roll_up_condition();
                    parcer_status.Pop();

                    expression(pos);
                }

                else if (c_operator_rating(pos) < rat_semicolon & c_operator_rating(pos) >= rat_eol)
                {
                    roll_up_condition();
                    parcer_status.Pop();
                    if (expression_terminator.Peek() == rat_semicolon) expression_terminator.Pop();

                    expression(pos);
                }

                else put_error(lexer_line[pos], "Parcer: Undefined symbol in event; a cause or ; or && or || expected");
            }

            else if (parcer_status.Peek() == "cycle_condition")
            {
                if (c_operator_rating(pos) > rat_opening_sq_bracket | c_open_cv_bracket(pos))
                {
                    parcer_status.Pop();
                    parcer_status.Push("cycle_finish");
                    parcer_status.Push("event");
                    expression(pos);
                }

                else if (c_operator_rating(pos) == rat_opening_sq_bracket)
                {
                    parcer_status.Pop();
                    new_expression(rat_closing_sq_bracket, "cycle_body");
                }

                else put_error(lexer_line[pos], "Parcer: an undefined symbol in cycle description");

            }

            else if (parcer_status.Peek() == "cycle_body")
            {
                roll_up_cycle_condition();
                parcer_status.Pop();
                parcer_status.Push("cycle_finish");
                parcer_status.Push("event");
                expression(pos);
            }

            else if (parcer_status.Peek() == "cycle_finish")
            {
                roll_up_cycle_body();
                parcer_status.Pop();
                expression(pos);
            }

            else if (parcer_status.Peek() == "begin_expression")
            {
                if (c_pre_op(pos)) 
                { 
                    stack.Push(lexer_line[pos]); 
                    parcer_status.Push("math_operator");  
                    new_expression(rat_pre_operator, "pre_argument"); 
                }

                else if (c_const(pos)) 
                { 
                    push_next("math_operator", pos); comparison_times = 0; 
                }

                else if (c_name(pos)) 
                { 
                    parcer_status.Push("math_operator"); 
                    push_next("call_2arg_post_operator", pos); 
                }

                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "math_operator");

                else if (c_operator_rating(pos) <= expression_terminator.Peek() & c_operator_rating(pos) >= rat_eol) 
                { 
                    parcer_status.Pop(); expression(pos); 
                }

                else put_error(lexer_line[pos], "Parcer: Undefined symbol in expression beginning; a name or constant expected");
            }

            else if (parcer_status.Peek() == "math_operator")
            {
                // index
                // +++ 15.01.2017 Rel 0.6
                if (c_open_sq_bracket(pos)) { parcer_status.Push("index_expression"); expression(pos); }

                else if (c_operator_rating(lexer_line[pos]) <= expression_terminator.Peek() & c_operator_rating(lexer_line[pos]) >= rat_eol)
                {
                    parcer_status.Pop(); parcer_status.Pop();
                    int a = expression_terminator.Pop();

                    if (parcer_status.Count() > 0)
                        if (parcer_status.Peek() == "call_arg")
                        {
                            expression_terminator.Push(a);
                            expression(pos);
                        }
                        else if (parcer_status.Peek() == "pre_argument") expression(pos);
                        else if (parcer_status.Peek() == "event_condition") expression(pos);
                        else if (parcer_status.Peek() == "event_next") expression(pos);
                }

                else if (c_logical_or(pos)) push_next("logical_or_right_argument", pos);
                else if (c_logical_and(pos)) push_next("logical_and_right_argument", pos);
                else if (c_compare(pos)) { push_next("compare_right_argument", pos); comparison_times = 0; }
                else if (c_add(pos)) push_next("add_right_argument", pos);
                else if (c_mult(pos)) push_next("mult_right_argument", pos);
                else if (c_assign(pos) & stack.Peek().type == 'n')
                {
                    push_next("assign_right_argument", pos);
                    //if (variables_table.Where(p => p == stack.Peek().lexeme) == null) variables_table.Add(lexer_line[pos].lexeme);
                }
                else put_error(lexer_line[pos], "Math_operator: Undefined symbol in expression; an arithmetic or logical operator or a name as destination in assign expression expected");
            }

            else if (parcer_status.Peek() == "logical_or_right_argument")
            {
                if (c_pre_op(pos)) { stack.Push(lexer_line[pos]); parcer_status.Push("logical_or_operator"); new_expression(rat_pre_operator, "pre_argument"); }
                else if (c_const(pos)) { push_next("logical_or_operator", pos); comparison_times = 0; }

                else if (c_name(pos))
                {
                    parcer_status.Push("logical_or_operator"); push_next("call_2arg_post_operator", pos);
                    comparison_times = 0;
                }

                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "logical_or_operator");
                else
                    put_error(lexer_line[pos], 
                        string.Format("Logical_or_right_argument: Undefined symbol next to the '{0}' operator; name or constant or pre-operator or starting new expression '(' expected", 
                        lexer_line[pos].lexeme));
            }

            else if (parcer_status.Peek() == "logical_and_right_argument")
            {
                if (c_pre_op(pos)) { stack.Push(lexer_line[pos]); parcer_status.Push("logical_and_operator"); new_expression(rat_pre_operator, "pre_argument"); }
                else if (c_const(pos)) { push_next("logical_and_operator", pos); comparison_times = 0; }

                else if (c_name(pos)) 
                { 
                    parcer_status.Push("logical_and_operator"); push_next("call_2arg_post_operator", pos);
                    comparison_times = 0;
                }

                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "logical_and_operator");
                else
                    put_error(lexer_line[pos],
                        string.Format("Logical_and_right_argument: Undefined symbol next to the '{0}' operator; name or constant or pre-operator or starting new expression '(' expected",
                        lexer_line[pos].lexeme));
            }

            else if (parcer_status.Peek() == "compare_right_argument")
            {
                if (comparison_times > 0) put_error(lexer_line[pos], "Parcer: Only 2 args for comparison are allowed");
                else if (c_pre_op(pos)) { stack.Push(lexer_line[pos]); parcer_status.Push("compare_operator"); new_expression(rat_pre_operator, "pre_argument"); }
                else if (c_const(pos)) { push_next("compare_operator", pos); comparison_times++; }

                else if (c_name(pos)) 
                { 
                    parcer_status.Push("compare_operator"); push_next("call_2arg_post_operator", pos);
                    comparison_times++;
                }

                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "compare_operator");
                else
                    put_error(lexer_line[pos],
                        string.Format("Compare_right_argument: Undefined symbol next to the '{0}' operator; name or constant or pre-operator or starting new expression '(' expected",
                        lexer_line[pos].lexeme));
            }

            else if (parcer_status.Peek() == "add_right_argument")
                if (c_pre_op(pos)) { stack.Push(lexer_line[pos]); parcer_status.Push("add_operator"); new_expression(rat_pre_operator, "pre_argument"); }
                else if (c_const(pos)) push_next("add_operator", pos);
                else if (c_name(pos)) { parcer_status.Push("add_operator"); push_next("call_2arg_post_operator", pos); }
                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "add_operator");
                else
                    put_error(lexer_line[pos],
                        string.Format("Add_right_argument: Undefined symbol next to the '{0}' operator; name or constant or pre-operator or starting new expression '(' expected",
                        lexer_line[pos].lexeme));

            else if (parcer_status.Peek() == "mult_right_argument")
                if (c_pre_op(pos)) { stack.Push(lexer_line[pos]); parcer_status.Push("mult_operator"); new_expression(rat_pre_operator, "pre_argument"); }
                else if (c_const(pos)) push_next("mult_operator", pos);
                else if (c_name(pos)) { parcer_status.Push("mult_operator"); push_next("call_2arg_post_operator", pos); }
                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "mult_operator");
                else
                    put_error(lexer_line[pos],
                        string.Format("Mult_right_argument: Undefined symbol next to the '{0}' operator; name or constant or pre-operator or starting new expression '(' expected",
                        lexer_line[pos].lexeme));

            else if (parcer_status.Peek() == "assign_right_argument")
                if (c_pre_op(pos)) { stack.Push(lexer_line[pos]); parcer_status.Push("assign_operator"); new_expression(rat_pre_operator, "pre_argument"); }
                else if (c_const(pos)) push_next("assign_operator", pos);
                else if (c_name(pos)) { parcer_status.Push("assign_operator"); push_next("call_2arg_post_operator", pos); }
                else if (c_open_bracket(pos)) new_expression(rat_closing_bracket, "assign_operator");
                else
                    put_error(lexer_line[pos],
                        string.Format("Assign_right_argument: Undefined symbol next to the '{0}' operator; name or constant or pre-operator or starting new expression '(' expected",
                        lexer_line[pos].lexeme));

            else if (parcer_status.Peek() == "logical_or_operator")
                exp_double_operator(rat_logical_or_operator, c_operator_rating(pos), "logical_or_right_argument", "logical_and_right_argument", pos);
               
            else if (parcer_status.Peek() == "logical_and_operator")
                exp_double_operator(rat_logical_and_operator, c_operator_rating(pos), "logical_and_right_argument", "compare_right_argument", pos);
               
            else if (parcer_status.Peek() == "compare_operator")
                exp_double_operator(rat_compare_operator, c_operator_rating(pos), "compare_right_argument", "add_right_argument", pos);

            else if (parcer_status.Peek() == "add_operator")
                exp_double_operator(rat_add_operator, c_operator_rating(pos), "add_right_argument", "mult_right_argument", pos);

            else if (parcer_status.Peek() == "mult_operator")
                exp_double_operator(rat_mult_operator, c_operator_rating(pos), "mult_right_argument", "assign_right_argument", pos);

            else if (parcer_status.Peek() == "assign_operator")
                exp_double_operator(rat_mult_operator, c_operator_rating(pos), "assign_right_argument", "assign_right_argument", pos);

            else if (parcer_status.Peek() == "pre_argument") { roll_up_1_arg(); expression(pos); }

            else if (parcer_status.Peek() == "call_2arg_post_operator")
            {
                if (c_post_op(pos))
                {
                    push_next("post_operator", pos); expression(pos);
                }
                else if (c_open_bracket(pos))
                {
                    parcer_status.Push("call_arg_list");
                    expression(pos);
                }
                // +++ 4.11.2015 Rel 0.6
                else if (c_open_sq_bracket(pos))
                {
                    lexeme_record lexeme = stack.Pop();
                    if (lexeme.type == 'n') lexeme.lexeme = "v_" /*+ ((ProcessName != "") ? ProcessName + "." : "")*/ + lexeme.lexeme;
                    stack.Push(lexeme);

                    // Check var_table here
                    // -->

                    parcer_status.Push("index_expression");
                    expression(pos);
                }
                // --- 4.11.2015 Rel 0.6
                else if (c_name_const(pos))
                    put_error(lexer_line[pos], "Parcer: Unexpected name or constant has been met");
                else
                {
                    lexeme_record lexeme = stack.Pop();
                    if (lexeme.type == 'n') lexeme.lexeme = "v_" /*+ ((ProcessName != "") ? ProcessName + "." : "")*/ + lexeme.lexeme;
                    stack.Push(lexeme);

                    // Check var_table here
                    // -->

                    parcer_status.Pop(); expression(pos);
                }
            }

            // +++ 4.11.2015 Rel 0.6
            else if (parcer_status.Peek() == "index_expression")
            {
                if (c_open_sq_bracket(pos))
                {
                    //stack.Push(new lexeme_record('d', "0"));
                    //new_expression(rat_comma, "call_arg");
                    stack.Push(/*lexer_line[pos]*/ new lexeme_record('o', "[]"));
                    //parcer_status.Push("begin_expression");
                    //expression_terminator.Push(rat_closing_sq_bracket);
                    new_expression(rat_closing_sq_bracket, "index_expression");
                }

                else /*if (c_close_sq_bracket(pos))*/
                {
                    roll_up_2_args();
                    expression(pos);
                }
                //else if (c_comma(pos)) new_expression(rat_comma, "call_arg");
            }
            // --- 4.11.2015 Rel 0.6

            else if (parcer_status.Peek() == "call_arg_list")
            {
                if (c_open_bracket(pos))
                {
                    stack.Push(new lexeme_record('d', "0"));
                    new_expression(rat_comma, "call_arg");
                }

                else if (c_close_bracket(pos))
                    // 19.11.2017 It needs to check ':=' operator first
                    // Call := { name; call_arg_list }
                    // NamedProcessDefinition := { name; call_arg_list; ProcessDefinition }
                    // ProcessDefinition := { :=; expression || e }
                    parcer_status.Push("ProcessDefinition");
                else if (c_comma(pos)) new_expression(rat_comma, "call_arg");
            }

            else if (parcer_status.Peek() == "call_arg")
            {
                if (c_comma(pos) | c_close_bracket(pos))
                {
                    if (!(stack.Peek().type == 'd' & stack.Peek().lexeme == "0"))
                    {
                        lexeme_record a = stack.Pop();
                        lexeme_record count = stack.Pop();
                        stack.Push(a);
                        count.lexeme = Convert.ToString(Convert.ToInt32(count.lexeme) + 1);
                        stack.Push(count);
                    }

                    parcer_status.Pop();
                    expression_terminator.Pop();

                    expression(pos);
                }
            }

            // 19.11.2017 Named processes
            else if (parcer_status.Peek() == "ProcessDefinition")
            // ProcessDefinition := { :=; expression || e }
            {
                if (c_NamedProcess(pos))
                // ProcessDefinition := { :=; expression }
                {
                    parcer_status.Pop();
                    //roll_up_ProcessDefinition();
                    parcer_status.Push("ProcessDefinition");
                }
                else if (c_open_cv_bracket(pos))
                {
                    ProcessRecord process = roll_up_ProcessDefinition();
                    ProcessTable.Add(process.ProcessName, process);

                    parcer_status.Pop();
                    string process_name = ((ProcessName != "") ? ProcessName + "_" : "") + stack.Peek().lexeme;
                    Q_Parcer prc = new Q_Parcer(lexer_line, error_line, process_name, process.VariablesTable);

                    int stack_count = stack.Count();
                    bool doit = true;
                    //stack.Push(lexer_line[pos]);
                    while (doit & error_line.Count < 25)
                    {
                        if (c_open_cv_bracket(pos)) stack.Push(lexer_line[pos]);
                        prc.expression(pos);
                        pos = prc.CurrentPos;
                        if (c_close_cv_bracket(pos)) stack.Pop();
                        if (stack.Count() == stack_count)
                            doit = false;
                        else pos++;
                    }

                    CurrentAddress = prc.get_line().Count();
                    parcer_line.AddRange(prc.get_line());

                    parcer_status.Pop(); // call_2arg

                    //expression(currentPos = pos /*= pos +2*/ /* to pass "};"*/);
                    //pos += 2; // to pass "};"
                    //parcer_status.Pop(); // math_operator
                    //parcer_status.Pop(); // begin_expression
                    //parcer_status.Pop(); // event_condition
                    //parcer_status.Push("ProcessDefinition_Finished_ClosingBracket");
                }
                else
                // ProcessDefinition := e
                {
                    parcer_status.Pop();
                    roll_up_call();
                    expression(pos);
                }
            }
            // 11.08.2018 Named processes
            //else if (parcer_status.Peek() == "ProcessDefinition_Finished_ClosingBracket")
            //{
            //
            //}


                currentPos = pos;
        }

    }
}
