using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_LexerTester
{
    public class gen_variable_record
    {
        public string name;
        public string value;
        //public string inner_name;

        public gen_variable_record(string vname, string vvalue)
        {
            name = vname;
            value = vvalue;
        }

        public override string ToString()
        {
            string res = name + "\t" + value + "\n";

            return res;
        }
    }


    class Q_Gen
    {
        Dictionary<string, gen_variable_record> variables = new Dictionary<string, gen_variable_record>();
        Int32 system_names_count = 0;

        List<string> gen_line = new List<string>();

        parcer_record[] parcer_line;
        Stack<parcer_record> ParcerStack = new Stack<parcer_record>();

        string ProcessName;

        public string[] get_line() { finalize();  return gen_line.ToArray(); }

        public Q_Gen(parcer_record[] parcer_l, string process_name = "")
        {
            parcer_line = parcer_l;
            ProcessName = (process_name != "") ? process_name + "." : "";

            variables.Add("v_" /*+ ProcessName*/ + "open", new gen_variable_record("#O", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "close", new gen_variable_record("#C", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "high", new gen_variable_record("#H", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "low", new gen_variable_record("#L", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "typical", new gen_variable_record("#T", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "median", new gen_variable_record("#M", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "time", new gen_variable_record("#X", "00:00_00.00.00"));
            variables.Add("v_" /*+ ProcessName*/ + "volume", new gen_variable_record("#V", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "account", new gen_variable_record("#A", "l0"));
            variables.Add("v_" /*+ ProcessName*/ + "money", new gen_variable_record("#N", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "equity", new gen_variable_record("#E", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "start_equity", new gen_variable_record("#SE", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "max_equity", new gen_variable_record("#XE", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "min_equity", new gen_variable_record("#NE", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "profit", new gen_variable_record("#SP", "%0"));
            variables.Add("v_" /*+ ProcessName*/ + "max_profit", new gen_variable_record("#SX", "%0"));
            variables.Add("v_" /*+ ProcessName*/ + "min_profit", new gen_variable_record("#SN", "%0"));
            variables.Add("v_" /*+ ProcessName*/ + "pos.price", new gen_variable_record("#R", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "pos.age", new gen_variable_record("#G", "c0"));
            variables.Add("v_" /*+ ProcessName*/ + "pos.profit", new gen_variable_record("#P", "%0"));
            variables.Add("v_" /*+ ProcessName*/ + "pos.abs_profit", new gen_variable_record("#AP", "p0"));
            //31.01.2016 Not supported variables.Add("pos.loss", new gen_variable_record("#S", "%0"));
            variables.Add("v_" /*+ ProcessName*/ + "log.status", new gen_variable_record("#log_status", "b0"));
            variables.Add("v_" /*+ ProcessName*/ + "log.level", new gen_variable_record("#log_level", "i0"));
            variables.Add("v_" /*+ ProcessName*/ + "signal.OpenLong", new gen_variable_record("#SOL", "n0"));
            variables.Add("v_" /*+ ProcessName*/ + "signal.CloseLong", new gen_variable_record("#SCL", "n0"));
            variables.Add("v_" /*+ ProcessName*/ + "signal.OpenShort", new gen_variable_record("#SOS", "n0"));
            variables.Add("v_" /*+ ProcessName*/ + "signal.CloseShort", new gen_variable_record("#SCS", "n0"));
            // 10.09.2018
            variables.Add("v_" /*+ ProcessName*/ + "last.price", new gen_variable_record("#LR", "p0"));
            variables.Add("v_" /*+ ProcessName*/ + "last.age", new gen_variable_record("#LG", "c0"));
            variables.Add("v_" /*+ ProcessName*/ + "last.profit", new gen_variable_record("#LP", "%0"));
            variables.Add("v_" /*+ ProcessName*/ + "last.abs_profit", new gen_variable_record("#LAP", "p0"));

            system_names_count = variables.Count();
        }

        public void finalize()
        {
            int i = variables.Count;
            while (i > system_names_count)
            {
                i--;
                string str = variables.Values.ElementAt(i).ToString();
                gen_line.Insert(0, variables.Values.ElementAt(i).ToString());
            }
        }

        public void gen(Int32 pos)
        {
            parcer_record r = parcer_line[pos];

            if (r.code == "||") r.code = "e|";

            if (r.code == "&&") r.code = "e&";

            if (r.code == "-") r.code = "m-";

            // Changing variable names
            int j = r.arg.Count();
            while (j-- > 0)
                if (variables.ContainsKey(r.arg[j]))
                    r.arg[j] = variables[r.arg[j]].name;


            // Print parcer_line
            if (r.code == "+" | r.code == "m-" | r.code == "*" | r.code == "/" |
                r.code == ">" | r.code == "<" | r.code == ">=" | r.code == "<=" |
                r.code == "==" | r.code == "!=" | r.code == "#_" | r.code == "#^" |
                r.code == ">=" | r.code == "<=" | r.code == "&" | r.code == "|" |
                r.code == "=" | r.code == "+=" | r.code == "-=" | r.code == "*=" |
                r.code == "/=" | r.code == ".." | r.code == "e|" | r.code == "e&" |
                r.code == "[]" | r.code == "]&")

                gen_line.Add(r.ToString(2));

            else if (r.code == "-")
            {
                r.code = "m-";
                gen_line.Add(r.ToString(2));
            }

            // 13.08.2020 commented
            /*else if (r.code == "!" | r.code == "neg")
                gen_line.Add(r.ToString(1));

            else if (r.code == "long" | r.code == "short" | r.code == "log")
                gen_line.Add(r.ToString(1));

            else if (r.code == "stop")
                gen_line.Add(r.ToString(1));*/

            else if (r.code == "~")
                gen_line.Add(r.ToString(0));

            // 13.08.2020 commented
            /*else if (r.code == "ind")
            {
                if (r.arg[0] == "sar") gen_line.Add(r.ToString(4));
                else if (r.arg[0] == "atr") gen_line.Add(r.ToString(2));
                else if (r.arg[0] == "macd") gen_line.Add(r.ToString(5));
                else if (r.arg[0] == "pricechannel") gen_line.Add(r.ToString(3));
                else gen_line.Add(r.ToString(5));
            }*/

            else gen_line.Add(r.ToString(r.arg.Count()));
        }

    }
}
