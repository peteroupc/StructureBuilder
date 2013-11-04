/*
Written in 2009 by Peter O.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/

If you like this, you should donate to Peter O.
at: http://upokecenter.com/d/
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StructureBuilder
{
	
	internal class CodeBuilder {
		StringBuilder sb;
		StringBuilder linesb;
		bool isvb=false;
		int depth;
		static Regex VbDepthRaise=new Regex(@"^(Public|Friend|For\s+|While|Get|Set|Private\s+Sub|Private\s+Function|Private\s+Property)|(Then|Else)\s*$|\(\s*_\s*$",RegexOptions.IgnoreCase);
		static Regex CsDepthRaise=new Regex(@"\{\s*$|\(\s*$",RegexOptions.IgnoreCase);
		static Regex VbDepthReduce=new Regex(@"^\s*\)|^\s*Else|Next|End\s+(If|Function|Sub|Get|Class|Set|Property)\s*$",RegexOptions.IgnoreCase);
		static Regex CsDepthReduce=new Regex(@"^\)|^[^\{]*?\}\s*$|^\s*\}",RegexOptions.IgnoreCase);
		public CodeBuilder(){
			sb=new StringBuilder();
			linesb=new StringBuilder();
			
		}
		public bool IsVb { get { return isvb; }
			set { isvb=value; }}
		public CodeBuilder AppendFormat(string csharp, string vb, params object[] format){
			return Append(String.Format(System.Globalization.CultureInfo.InvariantCulture,isvb ? vb : csharp, format));
		}
		public CodeBuilder Append(string csharp, string vb){
			return Append(isvb ? vb : csharp);
		}
		public CodeBuilder Append(string str){
			for(int i=0;i<str.Length;i++){
				if(str[i]=='\n'){
					string ls=linesb.ToString();
					if((isvb ? VbDepthReduce : CsDepthReduce).Match(ls).Success){
						depth=Math.Max(depth-1,0);
					}
					sb.Insert(sb.Length,"\t",depth);
					sb.Append(ls);
					sb.Append(Environment.NewLine);
					linesb.Remove(0,linesb.Length);
					if((isvb ? VbDepthRaise : CsDepthRaise).Match(ls).Success){
						depth++;
					}
				} else if(str[i]=='\r'){
					// skip
				} else {
					linesb.Append(str[i]);
				}
			}
			return this;
		}
		public override string ToString(){
			if(linesb.Length>0){
				// flush
				Append("\n");
			}
			return sb.ToString();
		}
	}


	class Program
	{
		/* struct CompareRule
string name;
bool desc;
Value comparedValue;
[noistructure];
		 */
		internal sealed class CompareRule : IEquatable<CompareRule> {
			// Generated Code, Do Not Edit
			// Fields
			private string name=String.Empty;
			private bool desc;
			private Value comparedValue=new Value();
			// Properties
			public string Name {
				get { return this.name; }
				set { this.name=value; }
			}
			public bool Desc {
				get { return this.desc; }
				set { this.desc=value; }
			}
			public Value ComparedValue {
				get { return this.comparedValue; }
				set { this.comparedValue=value; }
			}
			// Constructors
			public CompareRule(){}
			public CompareRule(CompareRule other){
				if(other==null)
					throw new ArgumentNullException("other");
				this.name=other.name;
				this.desc=other.desc;
				this.comparedValue=other.comparedValue;
			}
			public CompareRule(
				string name,
				bool desc,
				Value comparedValue
			){
				this.name=name;
				this.desc=desc;
				this.comparedValue=comparedValue;
			}
			// Methods
			public override bool Equals(object obj){
				CompareRule other=obj as CompareRule;
				if(other==null) return false;
				return this.Equals(other);
			}
			public bool Equals(CompareRule other){
				if(other==null) return false;
				return (
					(this.name==null ? other.name==null : this.name.Equals(other.name)) &&
					this.desc==other.desc &&
					(this.comparedValue==null ? other.comparedValue==null : this.comparedValue.Equals(other.comparedValue))
				);
			}
			public override int GetHashCode(){
				int hashCode=17;
				unchecked {
					hashCode=37*hashCode+((this.name==null) ? 0 : this.name.GetHashCode());
					hashCode=37*hashCode+((this.desc) ? 1 : 0);
					hashCode=37*hashCode+((this.comparedValue==null) ? 0 : this.comparedValue.GetHashCode());
				}
				return hashCode;
			}
			public override string ToString(){
				return String.Format(System.Globalization.CultureInfo.InvariantCulture,
				                     "[CompareRule: name={0}, desc={1}, comparedValue={2}]",
				                     this.name, this.desc, this.comparedValue
				                    );
			}
		}
		/*endstruct*/
		/* struct Value
string type,name;
bool bigEndian;
int size;
[noistructure];
		 */
		internal sealed class Value : IEquatable<Value> {
			// Generated Code, Do Not Edit
			// Fields
			private string type=String.Empty;
			private string name=String.Empty;
			private bool bigEndian;
			private int size;
			// Properties
			public string Type {
				get { return this.type; }
				set { this.type=value; }
			}
			public string Name {
				get { return this.name; }
				set { this.name=value; }
			}
			public bool BigEndian {
				get { return this.bigEndian; }
				set { this.bigEndian=value; }
			}
			public int Size {
				get { return this.size; }
				set { this.size=value; }
			}
			// Constructors
			public Value(){}
			public Value(Value other){
				if(other==null)
					throw new ArgumentNullException("other");
				this.type=other.type;
				this.name=other.name;
				this.bigEndian=other.bigEndian;
				this.size=other.size;
			}
			public Value(
				string type,
				string name,
				bool bigEndian,
				int size
			){
				this.type=type;
				this.name=name;
				this.bigEndian=bigEndian;
				this.size=size;
			}
			// Methods
			public override bool Equals(object obj){
				Value other=obj as Value;
				if(other==null) return false;
				return this.Equals(other);
			}
			public bool Equals(Value other){
				if(other==null) return false;
				return (
					(this.type==null ? other.type==null : this.type.Equals(other.type)) &&
					(this.name==null ? other.name==null : this.name.Equals(other.name)) &&
					this.bigEndian==other.bigEndian &&
					this.size==other.size
				);
			}
			public override int GetHashCode(){
				int hashCode=17;
				unchecked {
					hashCode=37*hashCode+((this.type==null) ? 0 : this.type.GetHashCode());
					hashCode=37*hashCode+((this.name==null) ? 0 : this.name.GetHashCode());
					hashCode=37*hashCode+((this.bigEndian) ? 1 : 0);
					hashCode=37*hashCode+this.size;
				}
				return hashCode;
			}
			public override string ToString(){
				return String.Format(System.Globalization.CultureInfo.InvariantCulture,
				                     "[Value: type={0}, name={1}, bigEndian={2}, size={3}]",
				                     this.type, this.name, this.bigEndian, this.size
				                    );
			}
		}
		/*endstruct*/
		



		internal static bool isPrimitive(string t){
			t=convertType(t);
			return t=="byte" || t=="sbyte" || t=="short" || t=="ushort" || t=="bool" ||
				t=="uint" || t=="int" || t=="ulong" || t=="long" || t=="float" || t=="double" || t=="decimal";
		}
		public static string Gsub(string s, string pattern, string replacement){
			return Regex.Replace(s,pattern,replacement);
		}
		public delegate string GsubMethod(MatchData matchData);
		public static string Gsub(string s, string pattern, GsubMethod method){
			return Regex.Replace(s,pattern,delegate(Match match){
			                     	MatchData md=new MatchData();
			                     	md.SetMatch(match);
			                     	return method(md);
			                     });
		}
		public static string Sub(string s, string pattern, string replacement){
			return new Regex(pattern).Replace(s,replacement,1);
		}
		
		public class MatchData {
			Match m;
			public void SetMatch(Match m){
				this.m=m;
			}
			public bool Success {
				get { return (this.m==null) ? false : this.m.Success; }
			}
			public string this[int i]{
				get {
					if(this.m==null)return null;
					if(i>=this.m.Groups.Count)return null;
					return this.m.Groups[i].ToString();
				}
			}
		}
		public static bool HaveMatch(string s, string pattern){
			return new Regex(pattern).Match(s).Success;
		}
		public static bool HaveMatch(string s, string pattern, MatchData match){
			match.SetMatch(new Regex(pattern).Match(s));
			return match.Success;
		}
		public static bool HaveMatchIgnoreCase(string s, string pattern){
			return new Regex(pattern,RegexOptions.IgnoreCase).Match(s).Success;
		}
		public static bool HaveMatchIgnoreCase(string s, string pattern, MatchData match){
			match.SetMatch(new Regex(pattern,RegexOptions.IgnoreCase).Match(s));
			return match.Success;
		}


		private static bool isvb=false;
		internal static string convertType(string datatype){
			return convertType(datatype,false);
		}
		internal static string convertType(string datatype, bool isvb){
			string[] conversion=new string[]{
				"byte",isvb ? "Byte" : "byte",
				"Byte",isvb ? "Byte" : "byte",
				"BYTE",isvb ? "Byte" : "byte",
				"SByte",isvb ? "SByte" : "sbyte",
				"sbyte",isvb ? "SByte" : "sbyte",
				"word",isvb ? "UShort" : "ushort",
				"ushort",isvb ? "UShort" : "ushort",
				"UShort",isvb ? "UShort" : "ushort",
				"WORD",isvb ? "UShort" : "ushort",
				"decimal",isvb ? "Decimal" : "decimal",
				"Decimal",isvb ? "Decimal" : "decimal",
				"Short",isvb ? "Short" : "short",
				"short",isvb ? "Short" : "short",
				"SHORT",isvb ? "Short" : "short",
				"LONG",isvb ? "Integer" : "int",
				"Integer",isvb ? "Integer" : "int",
				"int",isvb ? "Integer" : "int",
				"Int32",isvb ? "Integer" : "int",
				"uint",isvb ? "UInteger" : "uint",
				"UInt32",isvb ? "UInteger" : "uint",
				"UInteger",isvb ? "UInteger" : "uint",
				"dword",isvb ? "UInteger" : "uint",
				"DWORD",isvb ? "UInteger" : "uint",
				"ulong",isvb ? "ULong" : "ulong",
				"ULong",isvb ? "ULong" : "ulong",
				"UInt64",isvb ? "ULong" : "ulong",
				"Long",isvb ? "Long" : "long",
				"long",isvb ? "Long" : "long",
				"Int64",isvb ? "Long" : "long",
				"string",isvb ? "String" : "string",
				"String",isvb ? "String" : "string",
				"float",isvb ? "Single" : "float",
				"Single",isvb ? "Single" : "float",
				"double",isvb ? "Double" : "double",
				"Double",isvb ? "Double" : "double",
				"object",isvb ? "Object" : "object",
				"Object",isvb ? "Object" : "object",
				"bool",isvb ? "Boolean" : "bool",
				"boolean",isvb ? "Boolean" : "bool",
				"Boolean",isvb ? "Boolean" : "bool"
			};
			int i=0;
			while(i<conversion.Length){
				if(datatype==conversion[i])datatype=conversion[i+1] ;
				i+=2;
			}
			return datatype;
		}
		
		internal static string MemberName(string name, bool isvb){
			CultureInfo invariant=CultureInfo.InvariantCulture;
			return isvb ? String.Format("m_{0}",name) : name;
		}
		
		internal static string WriteStructure(MatchData structMatch){
			CodeBuilder structBuilder=new CodeBuilder();
			CultureInfo invariant=CultureInfo.InvariantCulture;
			string structKind=structMatch[1];
			string structName=structMatch[2];
			string structDataString=structMatch[3];
			if(isvb){
				structDataString=Gsub(structDataString,@"\r?\n\s*\'","");
			}
			structBuilder.IsVb=isvb;
			string[] structData=structMatch[3].Split(new string[]{";"},StringSplitOptions.None);
			MatchData match=new MatchData();
			structBuilder.AppendFormat("/*{0} struct {1}\n","'{0} struct {1}\n",structKind,structName);
			List<Value> values=new List<Value>();
			bool immutable=false;
			bool noistructure=false;
			List<CompareRule> compareRules=new List<CompareRule>();
			foreach(string dataVar in structData){
				if(String.IsNullOrEmpty(dataVar))continue;
				string data;
				if(isvb){
					data=Gsub(dataVar,@"^(.+)\'.*",delegate(MatchData s){
					          	return s[1].ToString();
					          });
					// Somehow it treats newline as end of string
					if(HaveMatch(Sub(data,@"\n"," "),@"^\s+$"))continue;
					if(!HaveMatch(data,@"^\s*\'"))break;
					data=Gsub(data,@"\r?\n","\n");
					data=Gsub(data,@"\n\n","\n");
					data=Gsub(data,@"^\s*\'","");
				} else {
					data=Gsub(dataVar,@"\/\/.*","");
					// Somehow it treats newline as end of string
					if(HaveMatch(Sub(data,@"\n"," "),@"^\s+$"))continue;
					if(HaveMatch(data,@"\*\/"))break;
				}
				structBuilder.Append(dataVar,dataVar).Append(";");
				if(HaveMatch(data,@"^\s*([a-zA-Z0-9\.]+)(_be)?\s+(\w+(?:\[\d+\])?(?:\s*,\s*\w+(?:\[\d+\])?)*)\s*$",match)){
					bool bigendian =(match[2]!=null && match[2]=="_be") ? true : false;
					string datatype=match[1];
					string[] names=match[3].Split(new string[]{","},StringSplitOptions.None);
					foreach(string name in names){
						string tempName=Gsub(Gsub(name,@"^\s+",""),@"\s+$","");
						string realname=Sub(tempName,@"\[.*$","");
						int dims=HaveMatch(tempName,@"\[(\d+)\]",match) ? Convert.ToInt32(match[1],invariant) : 1;
						Value v=new Value(convertType(datatype,isvb),realname,bigendian,dims);
						values.Add(v);
					}
				} else if(HaveMatchIgnoreCase(data,@"^\s*\[immutable\]\s*$",match)){
					immutable=true;
				} else if(HaveMatchIgnoreCase(data,@"^\s*\[noistructure\]\s*$",match)){
					noistructure=true;
				} else if(HaveMatchIgnoreCase(data,@"^\s*\[orderby\]\s+(\w+(?:\s+descending)?(?:\s*,\s*\w+(?:\s+descending)?)*)\s*$",match)){
					string[] names=match[1].Split(new string[]{","},StringSplitOptions.None);
					foreach(string name in names){
						string tempName=Gsub(Gsub(name,@"^\s+",""),@"\s+$","");
						string realname=Sub(tempName,@"\s.*$","");
						bool desc=HaveMatchIgnoreCase(tempName,@"\s+descending",match);
						CompareRule cr=new Program.CompareRule();
						cr.Desc=desc;
						cr.Name=realname;
						compareRules.Add(cr);
					}
				}
			}
			foreach(CompareRule c in compareRules){
				foreach(Value v in values){
					if(v.Name==c.Name){
						c.ComparedValue=v;
						break;
					}
				}
			}
			structBuilder.Append("\n*/\n",("\n"));
			if(compareRules.Count>0){
				structBuilder.AppendFormat(
					("{1} sealed class {0} : {2}System.IEquatable<{0}>, System.IComparable<{0}> {{\n"),
					("{1} NotInheritable Class {0}\n{2}Implements IEquatable(Of {0})\nImplements IComparable(Of {0})\n"),
					structName,structKind=="public" ? (!isvb ? "public" : "Public") : (!isvb ? "internal" : "Friend"),
					noistructure ? "" : (!isvb ? "PeterO.IStructure, " : "Implements PeterO.IStructure\n"));
			} else {
				structBuilder.AppendFormat(("{1} sealed class {0} : {2}IEquatable<{0}> {{\n"),
				                           ("{1} NotInheritable Class {0}\n{2}Implements IEquatable(Of {0})\n"),
				                           structName,structKind=="public" ? (!isvb ? "public" : "Public") :
				                           (!isvb ? "internal" : "Friend"),
				                           noistructure ? "" : (!isvb ? "PeterO.IStructure, " : "Implements PeterO.IStructure\n"));
			}
			FieldsAndProperties(structBuilder,values,immutable);
			structBuilder.Append("// Constructors\n",("' Constructors\n"));
			structBuilder.AppendFormat(
				("public {0}(){{}}\n"),
				("Public Sub New()\nEnd Sub\n"),structName);
			// Copy constructor
			structBuilder.AppendFormat(
				("public {0}({0} other){{\n"),
				("Public Sub New(other As {0})\n"),structName);
			structBuilder.AppendFormat("if(other==null)\n",
			                           "If other Is Nothing Then\n",structName);
			structBuilder.AppendFormat("throw new ArgumentNullException(\"other\");\n",
			                           "Throw New ArgumentNullException(\"other\")\nEnd If\n",
			                           structName);
			foreach(Value value in values){
				if(value.Size==1){
					structBuilder.AppendFormat("this.{0}=other.{0};\n",
					                           ("Me.{0}=other.{0}\n"),MemberName(value.Name,isvb));
				} else {
					structBuilder.AppendFormat(("if(other.{0}==null){{\n"),
					                           ("If other.{0} Is Nothing Then\n"),MemberName(value.Name,isvb));
					structBuilder.AppendFormat("this.{0}=null;\n",
					                           ("Me.{0}=Nothing\n"),MemberName(value.Name,isvb));
					structBuilder.Append("} else {\n",("Else\n"));
					structBuilder.AppendFormat("this.{0}=new {1}[other.{0}.Length];\n",
					                           "Me.{0}=New {1}(other.{0}.Length - 1) {{}}\n",
					                           MemberName(value.Name,isvb),value.Type);
					structBuilder.AppendFormat("Array.Copy(other.{0},this.{0},this.{0}.Length);\n",
					                           ("Array.Copy(other.{0},Me.{0},Me.{0}.Length)\n"),
					                           MemberName(value.Name,isvb),value.Type);
					structBuilder.Append(("}\n"),("End If\n"));
				}
			}
			structBuilder.AppendFormat(("}}\n"),("End Sub\n"),structName);
			if(values.Count>0){
				structBuilder.AppendFormat(("public {0}(\n"),("Public Sub New( _\n"),structName);
				for(int i=0;i<values.Count;i++){
					if(i>0)structBuilder.Append(",\n",(", _\n"));
					if(values[i].Size==1)
						structBuilder.AppendFormat("{0} {1}",("{1} As {0}"),values[i].Type,MemberName(values[i].Name,isvb));
					else
						structBuilder.AppendFormat("{0}[] {1}",("{1} As {0}()"),values[i].Type,MemberName(values[i].Name,isvb));
				}
				structBuilder.AppendFormat(("\n){{\n"),(")\n"),structName);
				foreach(Value value in values){
					if(value.Size==1){
						structBuilder.AppendFormat("this.{0}={0};\n","Me.{0}={0}\n",MemberName(value.Name,isvb));
					} else {
						structBuilder.AppendFormat(("if({0}==null){{\n"),("If {0} Is Nothing Then\n"),MemberName(value.Name,isvb));
						structBuilder.AppendFormat("throw new ArgumentNullException(\"{0}\");\n",
						                           ("Throw New ArgumentNullException(\"{0}\")\n"),MemberName(value.Name,isvb));
						structBuilder.Append("} else {\n",("Else\n"));
						structBuilder.AppendFormat("this.{0}=new {1}[{2}];\n",
						                           "Me.{0}=New {1}({3}) {{}}\n",MemberName(value.Name,isvb),value.Type,
						                           value.Size,value.Size-1);
						structBuilder.AppendFormat("Array.Copy({0},this.{0},Math.Min({1},this.{0}.Length));\n",
						                           "Array.Copy({0},Me.{0},Math.Min({1},Me.{0}.Length))\n",MemberName(value.Name,isvb),value.Size);
						structBuilder.Append("}\n",("End If\n"));
					}
				}
				structBuilder.AppendFormat("}}\n","End Sub\n",structName);
			}
			if(!noistructure){
				structBuilder.AppendFormat("public {0}(System.IO.Stream stream){{\n","Public Sub New(stream As System.IO.Stream)\n",structName);
				if(immutable){
					structBuilder.Append("ReadInternal(new PeterO.BinaryIO(stream));\n","ReadInternal(New PeterO.BinaryIO(stream))\n");
				} else {
					structBuilder.Append("Read(new PeterO.BinaryIO(stream));\n","Read(New PeterO.BinaryIO(stream))\n");
				}
				structBuilder.Append("}\n","End Sub\n");
				structBuilder.AppendFormat("public {0}(PeterO.BinaryIO bio){{\n","Public Sub New(bio As PeterO.BinaryIO)\n",structName);
				if(immutable){
					structBuilder.Append("ReadInternal(bio);\n","ReadInternal(bio)\n");
				} else {
					structBuilder.Append("Read(bio);\n","Read(bio)\n");
				}
				structBuilder.Append("}\n","End Sub\n");
			}
			structBuilder.Append("// Methods\n","' Methods\n");
			structBuilder.Append("public override bool Equals(object obj){\n",
			                     "Public Overrides Function Equals(obj As Object) As Boolean\n");
			structBuilder.AppendFormat("{0} other=obj as {0};\n","Dim other As {0}=TryCast(obj,{0})\n",structName);
			structBuilder.Append("if(other==null) return false;\n",
			                     "If other Is Nothing Then\nReturn False\nEnd If\n");
			structBuilder.Append("if(this==other) return true;\n",
			                     "If Me Is other Then\nReturn True\nEnd If\n");
			structBuilder.Append("return this.Equals(other);\n","Return Me.Equals(other)\n");
			structBuilder.Append("}\n","End Function\n");
			structBuilder.AppendFormat("public bool Equals({0} other){{\n",
			                           "Public Overloads Function Equals(other As {0}) As Boolean Implements IEquatable(Of {0}).Equals\n",structName);
			structBuilder.Append("if(other==null) return false;\n",
			                     "If other Is Nothing Then\nReturn False\nEnd If\n");
			if(values.Count>8){
				structBuilder.Append("if(this==other) return true;\n",
				                     "If Me Is other Then\nReturn True\nEnd If\n");
			}
			foreach(Value value in values){
				if(value.Size>1){
					structBuilder.AppendFormat("if(this.{0}!=other.{0}){{\n",
					                           "If Me.{0} IsNot other.{0} Then\n",MemberName(value.Name,isvb));
					structBuilder.AppendFormat("if((this.{0}==null && other.{0}!=null) ||\n",
					                           "If (Me.{0} Is Nothing AndAlso other.{0} IsNot Nothing) OrElse _\n",MemberName(value.Name,isvb));
					structBuilder.AppendFormat("(this.{0}!=null && other.{0}==null) ||\n",
					                           "(Me.{0} IsNot Nothing AndAlso other.{0} Is Nothing) OrElse _\n",
					                           MemberName(value.Name,isvb));
					structBuilder.AppendFormat("(this.{0}.Length!=other.{0}.Length)) return false;\n",
					                           "(Me.{0}.Length <> other.{0}.Length) Then\nReturn False\nEnd If\n",
					                           MemberName(value.Name,isvb));
					structBuilder.AppendFormat("for(int i=0;i<this.{0}.Length;i++){{\n",
					                           "For i As Integer=0 To Me.{0}.Length - 1\n",MemberName(value.Name,isvb));
					if(convertType(value.Type)=="double")
						structBuilder.AppendFormat(
							("if(BitConverter.ToInt64(BitConverter.GetBytes(this.{0}[i]),0)!=BitConverter.ToInt64(BitConverter.GetBytes(other.{0}[i]),0)) return false;\n"),
							("If BitConverter.ToInt64(BitConverter.GetBytes(Me.{0}(i)),0)<>BitConverter.ToInt64(BitConverter.GetBytes(other.{0}(i)),0)) Then\nReturn False\nEnd If\n"),
							MemberName(value.Name,isvb));
					else if(convertType(value.Type)=="float")
						structBuilder.AppendFormat(
							("if(BitConverter.ToInt32(BitConverter.GetBytes(this.{0}[i]),0)!=BitConverter.ToInt32(BitConverter.GetBytes(other.{0}[i]),0)) return false;\n"),
							("If BitConverter.ToInt32(BitConverter.GetBytes(Me.{0}(i)),0)<>BitConverter.ToInt32(BitConverter.GetBytes(other.{0}(i)),0)) Then\nReturn False\nEnd If\n"),
							MemberName(value.Name,isvb));
					else if(isPrimitive(value.Type))
						structBuilder.AppendFormat(
							("if(this.{0}[i]!=other.{0}[i]) return false;\n"),
							("If Me.{0}(i)<>other.{0}(i) Then\nReturn False\nEnd If\n"),
							MemberName(value.Name,isvb));
					else {
						structBuilder.AppendFormat(
							"if(!(this.{0}[i]==null ? other.{0}[i]==null : this.{0}[i].Equals(other.{0}[i]))) return false;\n",
							"If Not (If(Me.{0}(i) Is Nothing,other.{0}(i) Is Nothing,Me.{0}(i).Equals(other.{0}(i)))) Then\nReturn False\nEnd If\n",
							MemberName(value.Name,isvb));
					}
					structBuilder.Append("}\n","Next\n");
					structBuilder.Append("}\n","End If\n");
				}
			}
			structBuilder.Append("return (\n",("Return ( _\n"));
			bool first=true;
			foreach(Value value in values){
				if(value.Size==1){
					if(!first)structBuilder.Append(" &&\n"," AndAlso _\n");
					if(value.Type=="double")
						structBuilder.AppendFormat(
							("BitConverter.ToInt64(BitConverter.GetBytes(this.{0}),0)==BitConverter.ToInt64(BitConverter.GetBytes(other.{0}),0)"),
							("BitConverter.ToInt64(BitConverter.GetBytes(Me.{0}),0) = BitConverter.ToInt64(BitConverter.GetBytes(other.{0}),0)"),
							MemberName(value.Name,isvb));
					else if(value.Type=="float")
						structBuilder.AppendFormat(
							"BitConverter.ToInt32(BitConverter.GetBytes(this.{0}),0)==BitConverter.ToInt32(BitConverter.GetBytes(this.{0}),0)",
							"BitConverter.ToInt32(BitConverter.GetBytes(Me.{0}),0) = BitConverter.ToInt32(BitConverter.GetBytes(other.{0}),0)",
							MemberName(value.Name,isvb));
					else if(isPrimitive(value.Type))
						structBuilder.AppendFormat("this.{0}==other.{0}","Me.{0} = other.{0}",MemberName(value.Name,isvb));
					else
						structBuilder.AppendFormat(
							"(this.{0}==null ? other.{0}==null : this.{0}.Equals(other.{0}))",
							"If(Me.{0} Is Nothing, other.{0} Is Nothing, Me.{0}.Equals(other.{0}))",
							MemberName(value.Name,isvb));
					first=false;
				}
			}
			if(first)
				structBuilder.Append("true",("True"));
			structBuilder.Append("\n",(" _\n"));
			structBuilder.Append(");\n",(")\n"));
			structBuilder.Append(("}\n"),("End Function\n"));
			HashCodeMethod(structBuilder,values);
			CompareToMethod(structBuilder,compareRules,structName);
			ToStringMethod(structBuilder,values,structName);
			if(!noistructure){
				if(immutable){
					structBuilder.Append("public void Read(System.IO.Stream stream){\n",
					                     "Public Sub Read(stream As System.IO.Stream) Implements PeterO.IStructure.Read\n");
					structBuilder.Append("throw new NotSupportedException();\n",
					                     "Throw New NotSupportedException()\n");
					structBuilder.Append("}\n","End Sub\n");
					structBuilder.Append("public void Read(PeterO.BinaryIO bio){\n",
					                     "Public Sub Read(bio As PeterO.BinaryIO) Implements PeterO.IStructure.Read\n");
					structBuilder.Append("throw new NotSupportedException();\n",
					                     "Throw New NotSupportedException()\n");
					structBuilder.Append("}\n","End Sub\n");
					structBuilder.Append("private void ReadInternal(PeterO.BinaryIO bio){\n",
					                     "Public Sub ReadInternal(bio As PeterO.BinaryIO)\n");
				} else {
					structBuilder.Append("public void Read(System.IO.Stream stream){\n",
					                     "Public Sub Read(stream As System.IO.Stream) Implements PeterO.IStructure.Read\n");
					structBuilder.Append("Read(new PeterO.BinaryIO(stream));\n",
					                    "Read(New PeterO.BinaryIO(stream))\n");
					structBuilder.Append("}\n","End Sub\n");
					structBuilder.Append("public void Read(PeterO.BinaryIO bio){\n",
					                     "Public Sub Read(bio As PeterO.BinaryIO) Implements PeterO.IStructure.Read\n");
				}
				structBuilder.Append("if(bio==null) throw new ArgumentNullException(\"bio\");\n",
				                     "If bio Is Nothing Then\nThrow New ArgumentNullException(\"bio\")\nEnd If\n");
				structBuilder.Append("unchecked {\n","");
				foreach(Value value in values){
					string type=convertType(value.Type);
					string lebe=(value.BigEndian) ? "BE" : "LE";
					string preamble=String.Format(invariant,"this.{0}=",MemberName(value.Name,isvb));
					if(isvb){
						preamble=String.Format(invariant,"Me.{0}=",MemberName(value.Name,isvb));
					}
					if(type=="byte" && value.Size>1){
						structBuilder.Append(preamble).AppendFormat(
							"bio.ReadBytes({0});\n","bio.ReadBytes({0})\n",value.Size);
						continue;
					}
					if(value.Size>1){
						structBuilder.AppendFormat("for(int i=0;i<this.{0}.Length;i++){{\n",
						                           "For i As Integer = 0 To Me.{0}.Length - 1\n",MemberName(value.Name,isvb));
						preamble=String.Format(invariant,"this.{0}[i]=",MemberName(value.Name,isvb));
						if(isvb){
							preamble=String.Format(invariant,"Me.{0}(i)=",MemberName(value.Name,isvb));
						}
					}
					if(type=="short" || type=="ushort"){
						if(type=="ushort"){
							structBuilder.Append(preamble).AppendFormat("(ushort)bio.ReadInt16{0}();\n","CUShort(bio.ReadInt16{0}())\n",lebe);
						} else{
							structBuilder.Append(preamble).AppendFormat("bio.ReadInt16{0}();\n","bio.ReadInt16{0}()\n",lebe);
						}
					} else if(type=="int" || type=="uint"){
						if(type=="uint"){
							structBuilder.Append(preamble).AppendFormat("(uint)bio.ReadInt32{0}();\n","CUInt(bio.ReadInt32{0}())\n",lebe);
						} else{
							structBuilder.Append(preamble).AppendFormat("bio.ReadInt32{0}();\n","bio.ReadInt32{0}()\n",lebe);
						}
					} else if(type=="long" || type=="ulong"){
						if(type=="ulong"){
							structBuilder.Append(preamble).AppendFormat("(ulong)bio.ReadInt64{0}();\n","CULng(bio.ReadInt64{0}())\n",lebe);
						} else{
							structBuilder.Append(preamble).AppendFormat("bio.ReadInt64{0}();\n","bio.ReadInt64{0}()\n",lebe);
						}
					} else if(type=="float"){
						structBuilder.Append(preamble).AppendFormat("bio.ReadSingle{0}();\n",("bio.ReadSingle{0}()\n"),lebe);
					} else if(type=="string"){
						structBuilder.Append(preamble).AppendFormat("bio.ReadString{0}();\n",("bio.ReadString{0}()\n"),lebe);
					} else if(type=="double"){
						structBuilder.Append(preamble).AppendFormat("bio.ReadDouble{0}();\n",("bio.ReadDouble{0}()\n"),lebe);
					} else if(type=="bool"){
						structBuilder.Append(preamble).AppendFormat("(bio.ReadByte()!=0);\n",("(bio.ReadByte{0}() <> 0)\n"),lebe);
					} else if(type=="decimal"){
						structBuilder.Append(preamble).AppendFormat("bio.ReadDecimal{0}();\n",("bio.ReadDecimal{0}()\n"),lebe);
					} else if(type=="byte" && value.Size>1){
						structBuilder.Append(preamble).AppendFormat("bio.ReadBytes({0});\n",("bio.ReadBytes({0})\n"),value.Size);
					} else if(type=="byte" || type=="sbyte"){
						if(type=="sbyte"){
							structBuilder.Append(preamble).AppendFormat("(sbyte)bio.ReadByte();\n","CType(bio.ReadByte(),SByte)\n",lebe);
						} else{
							structBuilder.Append(preamble).AppendFormat("bio.ReadByte();\n","bio.ReadByte()\n",lebe);
						}
					} else {
						if(value.Size<=1){
							structBuilder.Append(preamble).AppendFormat("new {0}();\n","New {0}()\n",type);
							structBuilder.AppendFormat("this.{0}.Read(bio);\n","Me.{0}.Read(bio)\n",MemberName(value.Name,isvb));
						}else{
							structBuilder.Append(preamble).AppendFormat("new {0}();\n","New {0}()\n",type);
							structBuilder.AppendFormat("this.{0}[i].Read(bio);\n","Me.{0}(i).Read(bio)\n",MemberName(value.Name,isvb));
						}
					}
					if(value.Size>1){
						structBuilder.Append("}\n","Next\n");
					}
				}
				structBuilder.Append("}\n","");
				structBuilder.Append("}\n","End Sub\n");
				if(isvb){
					structBuilder.Append("Public Sub Write(stream As System.IO.Stream) Implements PeterO.IStructure.Write\n");
					structBuilder.Append("Write(New PeterO.BinaryIO(stream))\n");
					structBuilder.Append("End Sub\n");
					structBuilder.Append("Public Sub Write(bio As PeterO.BinaryIO) Implements PeterO.IStructure.Write\n");
					structBuilder.Append("If bio Is Nothing Then\nThrow New ArgumentNullException(\"bio\")\nEnd If\n");
				} else {
					structBuilder.Append("public void Write(System.IO.Stream stream){\n");
					structBuilder.Append("Write(new PeterO.BinaryIO(stream));\n");
					structBuilder.Append("}\n");
					structBuilder.Append("public void Write(PeterO.BinaryIO bio){\n");
					structBuilder.Append("if(bio==null) throw new ArgumentNullException(\"bio\");\n");
					structBuilder.Append("unchecked {\n");
				}
				foreach(Value value in values){
					string type=convertType(value.Type);
					string lebe=(value.BigEndian) ? "BE" : "LE";
					string preamble="";
					if(type=="byte" && value.Size>1){
						structBuilder.Append(preamble).AppendFormat("bio.Write(this.{0});\n","bio.Write(Me.{0})\n",MemberName(value.Name,isvb));
						continue;
					}
					string thisValue=String.Format(invariant,
					                               isvb ? "Me.{0}" : "this.{0}",MemberName(value.Name,isvb));
					if(value.Size>1){
						structBuilder.AppendFormat("for(int i=0;i<this.{0}.Length;i++){{\n",
						                           "For i As Integer=0 To Me.{0}.Length - 1\n",MemberName(value.Name,isvb));
						preamble="";
						thisValue+=(isvb ? "(i)" : "[i]");
					}
					if(type=="short" || type=="int" || type=="long" || type=="float" || type=="double" || type=="decimal")
						structBuilder.Append(preamble).AppendFormat("bio.Write{0}({1});\n",("bio.Write{0}({1})\n"),lebe,thisValue);
					else if(type=="ushort")
						structBuilder.Append(preamble).AppendFormat("bio.Write{0}((short){1});\n",("bio.Write{0}(CShort({1}))\n"),lebe,thisValue);
					else if(type=="string")
						structBuilder.Append(preamble).AppendFormat("bio.WriteString{0}({1});\n","bio.WriteString{0}({1})\n",lebe,thisValue);
					else if(type=="uint")
						structBuilder.Append(preamble).AppendFormat("bio.Write{0}((int){1});\n","bio.Write{0}(CInt({1}))\n",lebe,thisValue);
					else if(type=="bool")
						structBuilder.Append(preamble).AppendFormat("bio.Write((byte)(({1}) ? 1 : 0));\n",
						                                            "bio.Write(CByte(If({1},1,0)))\n",lebe,thisValue);
					else if(type=="ulong")
						structBuilder.Append(preamble).AppendFormat("bio.Write{0}((long){1});\n","bio.Write{0}(CLng({1}))\n",lebe,thisValue);
					else if(type=="byte")
						structBuilder.Append(preamble).AppendFormat("bio.Write({0});\n","bio.Write({0})\n",thisValue);
					else if(type=="sbyte")
						structBuilder.Append(preamble).AppendFormat("bio.Write((byte){0});\n","bio.Write(CByte({0}))\n",thisValue);
					else
						structBuilder.Append(preamble).AppendFormat("{0}.Write(bio);\n","{0}.Write(bio)\n",thisValue);
					if(value.Size>1){
						structBuilder.Append("}\n",("Next\n"));
					}
				}
				structBuilder.Append("}\n","");
				structBuilder.Append("}\n","End Sub\n");
			}
			structBuilder.Append("}\n","End Class\n");
			structBuilder.Append("/*endstruct*/","' endstruct\n");
			return structBuilder.ToString();
		}
		
		static List<int> primeTable=null;
		static int[] GetTwoRandomPrimes(Random rnd){
			int[] ret=new int[2];
			do {
				ret[0]=GetRandomPrime(rnd);
				ret[1]=GetRandomPrime(rnd);
			} while(ret[0]==ret[1]);
			return ret;
		}
		static int GetRandomPrime(Random rnd){
			if(primeTable==null){
				primeTable=new List<int>();
        // intentionally starts with 17
				int testEntriesEnd=1;
				for(int p=17;p<=32719;p+=2){
					int j;
					for(j=1;j<testEntriesEnd;j++){
						if(p%primeTable[j]==0)break;
					}
					if(j==testEntriesEnd){
						primeTable.Add(p);
						testEntriesEnd=Math.Min(54,primeTable.Count);
					}
				}
			}
			int rn=rnd.Next(10,primeTable.Count);
			return primeTable[rn];
		}

		internal static void ToStringMethod (
			CodeBuilder structBuilder,
			IList<Value> values,
			string typeName
		)
		{
			System.Globalization.CultureInfo invariantCulture = System.Globalization.CultureInfo.InvariantCulture;
			structBuilder.Append("public override string ToString(){\n","Public Overrides Function ToString() As String\n");
			bool flag = false;
			int num = 0;
			while (num < values.Count)
			{
				if (values[num].Size > 1)
				{
					flag = true;
				}
				num = num + 1;
			}
			if (values.Count == 0)
			{
				structBuilder.AppendFormat("return \"[{0}]\";\n","Return \"[{0}]\"\n", new object[] { typeName });
			}
			else
			{
				Value value;
				if ((values.Count <= 10) && !flag)
				{
					structBuilder.AppendFormat("return String.Format(System.Globalization.CultureInfo.InvariantCulture,\n\"[{0}: ",
					                           "Return String.Format(System.Globalization.CultureInfo.InvariantCulture, _\n\"[{0}: ",
					                           new object[] { typeName });
					num = 0;
					while (num < values.Count)
					{
						value = values[num];
						if (num > 0)
						{
							structBuilder.Append(", ");
						}
						structBuilder.AppendFormat("{0}={{","{0}={{", new object[] { value.Name });
						structBuilder.AppendFormat("{0}","{0}", new object[] { num });
						structBuilder.Append("}");
						num = num + 1;
					}
					structBuilder.Append("]\",\n","]\", _\n");
					num = 0;
					while (num < values.Count)
					{
						value = values[num];
						if (num > 0)
						{
							structBuilder.Append(", ");
						}
						structBuilder.AppendFormat("this.{0}","Me.{0}", new object[] { MemberName(value.Name,structBuilder.IsVb) });
						num = num + 1;
					}
					structBuilder.Append("\n);\n","\n)\n");
				}
				else
				{
					structBuilder.Append("System.Text.StringBuilder builder=new System.Text.StringBuilder();\n",
					                     "Dim builder As New System.Text.StringBuilder()\n");
					structBuilder.Append("System.Globalization.CultureInfo invariant=System.Globalization.CultureInfo.InvariantCulture;\n",
					                     "Dim invariant As System.Globalization.CultureInfo=System.Globalization.CultureInfo.InvariantCulture\n");
					structBuilder.AppendFormat("builder.Append(\"[{0}: \");\n",
					                           "builder.Append(\"[{0}: \")\n",
					                           new object[] { typeName });
					string text = "";
					IEnumerator<Value> enumerator = values.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							value = enumerator.Current;
							if (value.Size > 1)
							{
								structBuilder.AppendFormat("builder.Append(\"{0}{1}=\");\n",
								                           "builder.Append(\"{0}{1}=\")\n",								                           new object[] { text, value.Name });
								structBuilder.AppendFormat("if(this.{0}==null){{ builder.Append(\"(null)\"); }}\n",
								                           "If Me.{0} Is Nothing Then\nbuilder.Append(\"(null)\")\n", new object[] { 
								                           	MemberName(value.Name,structBuilder.IsVb) });
								structBuilder.AppendFormat(
									"else {{\nbuilder.Append(\"{{\");\nbool first=true;\n",
									"Else\nbuilder.Append(\"{{\")\nDim first As Boolean=True\n",
									new object[] { value.Name });
								structBuilder.AppendFormat(
									"foreach({0} obj in this.{1}){{\n",
									"For Each obj As {0} In Me.{1}\n",
									new object[] { value.Type, MemberName(value.Name,structBuilder.IsVb) });
								structBuilder.AppendFormat("if(!first) builder.Append(\",\");\n",
								                           "If Not first Then\nbuilder.Append(\",\")\nEnd If\n",
								                           new object[] { value.Type, value.Name });
								structBuilder.AppendFormat("builder.AppendFormat(invariant,\"{{0}}\",obj);\n",
								                           "builder.AppendFormat(invariant,\"{{0}}\",obj)\n",
								                           new object[] { value.Type, value.Name });
								structBuilder.AppendFormat("first=false;\n","first=False\n", new object[] { value.Type, value.Name });
								structBuilder.AppendFormat("}}\n","Next\n", new object[] { value.Type, value.Name });
								structBuilder.AppendFormat("builder.Append(\"}}\");\n","builder.Append(\"}}\")\n", new object[] { value.Name });
								structBuilder.AppendFormat("}}\n","End If\n", new object[] { value.Name });
							}
							else
							{
								structBuilder.AppendFormat("builder.AppendFormat(invariant,\"{0}{1}={{0}}\",this.{2});\n",
								                           "builder.AppendFormat(invariant,\"{0}{1}={{0}}\",Me.{2})\n",
								                           new object[] { text, value.Name, MemberName(value.Name,structBuilder.IsVb) });
							}
							text = ", ";
						}
					}
					finally
					{
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					structBuilder.Append("builder.Append(\"]\");\n","builder.Append(\"]\")\n");
					structBuilder.Append("return builder.ToString();\n","Return builder.ToString()\n");
				}
			}
			structBuilder.Append("}\n","End Function\n");
		}
		
		internal static void CompareToMethod (
			CodeBuilder structBuilder,
			IList<CompareRule> values,
			string typeName
		)
		{
			if (values.Count != 0)
			{
				structBuilder.AppendFormat(
					"public int CompareTo({0} other){{\n",
					"Public Function CompareTo(other As {0}) As Integer Implements IComparable(Of {0}).CompareTo\n",new object[] { typeName });
				structBuilder.Append("int cmp=-1;\n","Dim cmp As Integer=-1\n");
				structBuilder.Append("if(other==null)return cmp;\n",
				                     "If other Is Nothing Then\nReturn cmp\nEnd If\n");
				string text4 = "cmp";
				string text5 = "cmp";
				foreach (CompareRule current in values)
				{
					if (current.ComparedValue == null)
					{
						continue;
					}
					Value comparedValue;
					comparedValue = current.ComparedValue;
					if(comparedValue.Name=="" || comparedValue.Type==""){
						continue;
					}
					string text = !current.Desc ? (isvb ? "Me" : "this") : "other";
					string text2 = string.Concat(!current.Desc ? "other" : (isvb ? "Me" : "this"), ".", MemberName(comparedValue.Name,structBuilder.IsVb));
					text = string.Concat(text, ".", MemberName(comparedValue.Name,structBuilder.IsVb));
					string text3 = "";
					if (comparedValue.Size > 1)
					{
						text3 = "";
						structBuilder.AppendFormat("if({1}!={2}){{\n",
						                           "If {1} IsNot {2} Then\n",
						                           new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text, text2 });
						structBuilder.AppendFormat("if({1}!=null && {2}==null) return -1;\n",
						                           "If {1} IsNot Nothing AndAlso {2} Is Nothing Then\nReturn -1\nEnd If\n",
						                           new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text, text2 });
						structBuilder.AppendFormat("if({1}==null && {2}!=null) return 1;\n",
						                           "If {1} Is Nothing AndAlso {2} IsNot Nothing Then\nReturn 1\nEnd If\n",
						                           new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text, text2 });
						structBuilder.AppendFormat("if({1}.Length!={2}.Length)\nreturn ({1}.Length<{2}.Length) ? -1 : 1;\n",
						                           "If {1}.Length<>{2}.Length Then\nReturn If({1}.Length<{2}.Length,-1,1)\nEnd If\n",
						                           new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text, text2 });
						structBuilder.AppendFormat("for(int i=0;i<{1}.Length;i++){{\n",
						                           "For i As Integer=0 To {1}.Length - 1\n",
						                           new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text });
						text = string.Concat(text, structBuilder.IsVb ? "(i)" : "[i]");
						text2 = string.Concat(text2, structBuilder.IsVb ? "(i)" : "[i]");
					}
					if ((convertType(comparedValue.Type) == "float") || (convertType(comparedValue.Type) == "double"))
					{
						structBuilder.AppendFormat("{1}={2}.CompareTo({3});\n{4}if({0}!=0)\n{4}return {0};\n",
						                           "{1}={2}.CompareTo({3})\nIf {0}<>0 Then\nReturn {0}\nEnd If\n",
						                           new object[] { text5, text4, text, text2, text3 });
						text4=text5;
					}
					else if (convertType(comparedValue.Type) == "bool")
					{
						structBuilder.AppendFormat("if({1}!={2})\n{0}return (({1} ? 1 : 0)<({2} ? 1 : 0)) ? -1 : 1;\n",
						                           "If {1}<>{2} Then\nReturn If(If({1},1,0)<If({2},1,0),-1,1)\nEnd If\n",
						                           new object[] { text3, text, text2 });
					}
					else if (Program.isPrimitive(comparedValue.Type))
					{
						structBuilder.AppendFormat("{0}if({1}!={2})\n{0}return ({1}<{2}) ? -1 : 1;\n",
						                           "{0}If {1}<>{2} Then\nReturn If({1}<{2},-1,1)\nEnd If\n",
						                           new object[] { text3, text, text2 });
					}
					else if (convertType(comparedValue.Type) == "string")
					{
						structBuilder.AppendFormat(
							"{1}=String.Compare({2},{3},StringComparison.Ordinal);\n{4}if({0}!=0)\n{4}return {0};\n",
							"{1}=[String].Compare({2},{3},StringComparison.Ordinal)\nIf {0}<>0 Then\nReturn {0}\nEnd If\n",
							new object[] { text5, text4, text, text2, text3 });
						text4=text5;
					}
					else
					{
						structBuilder.AppendFormat(
							"{1}=System.Collections.Generic.Comparer<{2}>.Default.Compare({3},{4});\nif({0}!=0)\nreturn {0};\n",
							"{1}=System.Collections.Generic.Comparer(Of {2}).[Default].Compare({3},{4})\nIf {0}<>0 Then\nReturn {0}\nEnd If\n",
							new object[] { text5, text4, comparedValue.Type, text, text2 });
						text4=text5;
					}
					if (comparedValue.Size > 1)
					{
						structBuilder.AppendFormat("}}\n","Next\n", new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text });
						structBuilder.AppendFormat("}}\n","End If\n", new object[] { MemberName(comparedValue.Name,structBuilder.IsVb), text });
					}
				}
				structBuilder.Append("return 0;\n","Return 0\n");
				structBuilder.Append("}\n","End Function\n");
			}
		}


		internal static void HashCodeMethod(CodeBuilder structBuilder, IList<Value> values){
			Random rnd=new Random(Environment.TickCount^structBuilder.ToString().GetHashCode());
			int[] primes=GetTwoRandomPrimes(rnd);
			structBuilder.Append("public override int GetHashCode(){\n",
			                     "Public Overrides Function GetHashCode() As Integer\n");
			structBuilder.AppendFormat("int hashCode={0};\n","Dim hashCode As Integer={0}\n",primes[0]);
			structBuilder.Append("unchecked {\n","");
			foreach(Value value in values){
				string indent="";
				string thisValue=String.Format(System.Globalization.CultureInfo.InvariantCulture,
				                               structBuilder.IsVb ? "Me.m_{0}" : "this.{0}",value.Name);
				string hashCodeVar=String.Format(System.Globalization.CultureInfo.InvariantCulture,"hashCode={0}*hashCode+",primes[1]);
				if(value.Size>1){
					indent="";
					structBuilder.AppendFormat("hashCode*={0};\n","hashCode=hashCode*{0}\n",primes[1]);
					structBuilder.AppendFormat("if({0}!=null){{\n","If {0} IsNot Nothing Then\n",thisValue);
					structBuilder.AppendFormat("int hashCodeSub={0};\n","Dim hashCodeSub As Integer={0}\n",primes[0]);
					structBuilder.AppendFormat("foreach({0} value in {1}){{\n","For Each value As {0} In {1}\n",
					                           value.Type,thisValue);
					hashCodeVar=String.Format(System.Globalization.CultureInfo.InvariantCulture,
					                          structBuilder.IsVb ? "hashCodeSub={0}*hashCodeSub+" : "hashCodeSub={0}*hashCodeSub+",primes[1]);
					thisValue="value";
				}
				string convertedType=convertType(value.Type);
				if (convertedType=="ushort" || convertedType=="uint" || 
				    convertedType=="byte" || convertedType=="sbyte" || convertedType=="char" || convertedType=="short"){
					structBuilder.AppendFormat("{0}{1}(int){2};\n","{1}CInt({2})\n",String.Empty,hashCodeVar,thisValue);
				} else if (convertedType=="int"){
					structBuilder.AppendFormat("{0}{1}{2};\n","{1}{2}\n",String.Empty,hashCodeVar,thisValue);
				} else if (convertedType=="long" || convertedType=="ulong"){
					structBuilder.AppendFormat("{0}{1}(int)({2}^({2}>>32));\n","{1}CInt(CULng(CULng({2} Xor ({2}>>32)) And CULng(&HFFFFFFFF)))\n",String.Empty,hashCodeVar,thisValue);
				} else if (convertedType=="bool"){
					structBuilder.AppendFormat("{0}{1}(({2}) ? 1 : 0);\n","{1}If({2},1,0)\n",String.Empty,hashCodeVar,thisValue);
				} else if (convertedType=="float"){
					structBuilder.AppendFormat("{0}if(BitConverter.IsLittleEndian){{\n","If BitConverter.IsLittleEndian Then\n",indent);
					structBuilder.AppendFormat("{0}byte[] floatbits=BitConverter.GetBytes({1});\n",
					                           "Dim floatbits As Byte()=BitConverter.GetBytes({1})\n",String.Empty,thisValue);
					structBuilder.AppendFormat("{0}{1}((int)floatbits[0]|((int)floatbits[1]<<8)|((int)floatbits[2]<<16)|((int)floatbits[3]<<24));\n",
					                           "{0}{1}(CInt(floatbits(0)) Or (CInt(floatbits(1))<<8) Or (CInt(floatbits(2))<<16) Or (CInt(floatbits(3))<<24))\n",
					                           
					                           String.Empty,hashCodeVar);
					structBuilder.AppendFormat("{0}}} else {{\n","Else\n",indent);
					structBuilder.AppendFormat("{0}byte[] floatbits=BitConverter.GetBytes({1});\n",
					                           "Dim floatbits As Byte()=BitConverter.GetBytes({1})\n",String.Empty,thisValue);
					structBuilder.AppendFormat("{0}{1}((int)floatbits[3]|((int)floatbits[2]<<8)|((int)floatbits[1]<<16)|((int)floatbits[0]<<24));\n",
					                           "{0}{1}(CInt(floatbits(3)) Or (CInt(floatbits(2))<<8) Or (CInt(floatbits(1))<<16) Or (CInt(floatbits(0))<<24))\n",
					                           String.Empty,hashCodeVar);
					structBuilder.AppendFormat("{0}}}\n","End If\n",indent);
				} else if (convertedType=="double"){
					structBuilder.AppendFormat("{0}if(BitConverter.IsLittleEndian){{\n","If BitConverter.IsLittleEndian Then\n",indent);
					structBuilder.AppendFormat("{0}byte[] floatbits=BitConverter.GetBytes({1});\n",
					                           "Dim floatbits As Byte()=BitConverter.GetBytes({1})\n",String.Empty,thisValue);
					structBuilder.AppendFormat("{0}{1}((int)floatbits[0]|((int)floatbits[1]<<8)|((int)floatbits[2]<<16)|((int)floatbits[3]<<24));\n",
					                           "{0}{1}(CInt(floatbits(0)) Or (CInt(floatbits(1))<<8) Or (CInt(floatbits(2))<<16) Or (CInt(floatbits(3))<<24))\n",String.Empty,hashCodeVar);
					structBuilder.AppendFormat("{0}{1}((int)floatbits[4]|((int)floatbits[5]<<8)|((int)floatbits[6]<<16)|((int)floatbits[7]<<24));\n",
					                           "{0}{1}(CInt(floatbits(4)) Or (CInt(floatbits(5))<<8) Or (CInt(floatbits(6))<<16) Or (CInt(floatbits(7))<<24))\n",String.Empty,hashCodeVar);
					structBuilder.AppendFormat("{0}}} else {{\n","Else\n",indent);
					structBuilder.AppendFormat("{0}byte[] floatbits=BitConverter.GetBytes({1});\n",
					                           "Dim floatbits As Byte()=BitConverter.GetBytes({1})\n",String.Empty,thisValue);
					structBuilder.AppendFormat("{0}{1}((int)floatbits[7]|((int)floatbits[6]<<8)|((int)floatbits[5]<<16)|((int)floatbits[4]<<24));\n",
					                           "{0}{1}(CInt(floatbits(7)) Or (CInt(floatbits(6))<<8) Or (CInt(floatbits(5))<<16) Or (CInt(floatbits(4))<<24))\n",String.Empty,hashCodeVar);
					structBuilder.AppendFormat("{0}{1}((int)floatbits[3]|((int)floatbits[2]<<8)|((int)floatbits[1]<<16)|((int)floatbits[0]<<24));\n",
					                           "{0}{1}(CInt(floatbits(3)) Or (CInt(floatbits(2))<<8) Or (CInt(floatbits(1))<<16) Or (CInt(floatbits(0))<<24))\n",String.Empty,hashCodeVar);
					structBuilder.AppendFormat("{0}}}\n","End If\n",indent);
				} else if(convertedType=="decimal"){
					structBuilder.AppendFormat("{0}{1}{2}.GetHashCode();\n",
					                           "{1}{2}.GetHashCode()\n",String.Empty,hashCodeVar,thisValue);
				} else{
					structBuilder.AppendFormat("{0}{1}(({2}==null) ? 0 : {2}.GetHashCode());\n",
					                           "{1}If({2} Is Nothing,0,{2}.GetHashCode())\n",String.Empty,hashCodeVar,thisValue);
				}
				if(value.Size>1){
					structBuilder.Append("}\n","Next\n");
					structBuilder.Append("hashCode+=hashCodeSub;\n","hashCode=hashCode+hashCodeSub\n");
					structBuilder.Append("}\n","End If\n");
				}
			}
			structBuilder.Append("}\n","");
			structBuilder.Append("return hashCode;\n","Return hashCode\n");
			structBuilder.Append("}\n","End Function\n");
		}
		internal static void ParseStructures(string fileName){
			if(!File.Exists(fileName))return;
			Console.WriteLine("Processing {0}",fileName);
			string fileString=File.ReadAllText(fileName);
			string newString=fileString;
			newString=Gsub(newString,@"\r\r","\r");
			newString=Gsub(newString,@"\r(?!\n)","");
			if(Path.GetExtension(fileName)==".vb"){
				isvb=true;
				newString=Gsub(newString,@"\'\s*(?:(public)\s+)?struct\s+(\w+)\s*([\s\S]*?)\'\s*endstruct.*\n?",WriteStructure);
			} else {
				isvb=false;
				newString=Gsub(newString,@"//[^\n]\r?\n","");
				newString=Gsub(newString,@"\/\*\s*(?:(public)\s+)?struct\s+(\w+)\s*([\s\S]*?)endstruct\s*\*\/",WriteStructure);
			}
			if(!fileString.Equals(newString)){
				File.WriteAllText(fileName,newString);
			}
		}
		internal static void FieldsAndProperties(
			CodeBuilder structBuilder, IList<Value> values, bool readOnly
		){
			structBuilder.Append("// Generated Code, Do Not Edit\n","' Generated Code, Do Not Edit\n");
			structBuilder.Append("// Fields\n","' Fields\n");
			foreach(Value v in values){
				v.Name=v.Name.Substring(0,1).ToLower()+v.Name.Substring(1);
				string type=v.Type;
				if(v.Size>1)
					type+=(structBuilder.IsVb ? "()" : "[]");
				structBuilder.AppendFormat("private {0} {1}","Private {1} As {0}",type,MemberName(v.Name,structBuilder.IsVb));
				if(!isPrimitive(v.Type) && convertType(v.Type)!="string")
					structBuilder.AppendFormat("=new {0}()","=New {0}()",v.Type);
				else if(convertType(v.Type)=="string")
					structBuilder.AppendFormat("=String.Empty","=[String].Empty",v.Type);
				else if(v.Size>1)
					structBuilder.AppendFormat("=new {0}[{1}]","=New {0}({2}) {{}}",v.Type,v.Size,v.Size-1);
				structBuilder.Append(";\n","\n");
			}
			structBuilder.Append("// Properties\n","' Properties\n");
			foreach(Value v in values){
				string xx=v.Name.Substring(0,1).ToUpper()+v.Name.Substring(1);
				if(v.Size==1){
					structBuilder.AppendFormat("public {0} {1} {{\n",
					                           "Public {2}Property {1}() As {0}\n",v.Type,xx,
					                           readOnly ? "ReadOnly " : "");
					structBuilder.AppendFormat("get {{ return this.{0}; }}\n","Get\nReturn Me.{0}\nEnd Get\n",MemberName(v.Name,structBuilder.IsVb));
					if(!readOnly)
						structBuilder.AppendFormat("set {{ this.{0}=value; }}\n","Set\nMe.{0}=Value\nEnd Set\n",MemberName(v.Name,structBuilder.IsVb));
					structBuilder.Append("}\n","End Property\n");
				} else {
					if(readOnly){
						structBuilder.AppendFormat("public System.Collections.Generic.IList<{0}> {1} {{\n",
						                           "Public ReadOnly Property {1}() As System.Collections.Generic.IList(Of {0})\n",v.Type,xx);
						structBuilder.AppendFormat("get {{ return new System.Collections.ObjectModel.ReadOnlyCollection<{1}>(this.{0}); }}\n",
						                           "Get\nReturn New System.Collections.ObjectModel.ReadOnlyCollection(Of {1})(Me.{0})\nEnd Get\n",
						                           MemberName(v.Name,structBuilder.IsVb),v.Type);
						structBuilder.AppendFormat("}}\n","End Property\n");
						structBuilder.AppendFormat("public {0}[] {1}Array(){{\n","Public Function {1}Array() As {0}()\n",v.Type,xx);
						structBuilder.AppendFormat("return new System.Collections.Generic.List<{1}>(this.{0}).ToArray();\n",
						                           "Return New System.Collections.Generic.List(Of {1})(Me.{0}).ToArray()\n",MemberName(v.Name,structBuilder.IsVb),
						                          v.Type);
						structBuilder.Append("}\n","End Function\n");
					} else {
						structBuilder.AppendFormat("public System.Collections.Generic.IList<{0}> {1} {{\n",
						                           "Public ReadOnly Property {1}() As System.Collections.Generic.IList(Of {0})\n",v.Type,xx);
						structBuilder.AppendFormat("get {{ return this.{0}; }}\n",
						                           "Get\nReturn Me.{0}\nEnd Get\n",MemberName(v.Name,structBuilder.IsVb));
						structBuilder.AppendFormat("}}\n","End Property\n");
						structBuilder.AppendFormat("public {0}[] {1}Array(){{\n","Public Function {1}Array() As {0}()\n",v.Type,xx);
						structBuilder.AppendFormat("return this.{0};\n","Return Me.{0}\n",MemberName(v.Name,structBuilder.IsVb));
						structBuilder.Append("}\n","End Function\n");
					}
				}
			}
		}
		
		public static void Usage(){

			Console.WriteLine(
				"StructureBuilder.exe"+Environment.NewLine+
				"Written by Peter O."+Environment.NewLine+
				""+Environment.NewLine+
				"This program scans all C# and Visual Basic source files in the folder it's found in"+Environment.NewLine+
				"for comments that define data structures.  It will generate the structure's code."+Environment.NewLine+
				"A data structure implements the IStructure interface and is easy to read and"+Environment.NewLine+
				"write from data streams.  Each data structure is specified as internal to the"+Environment.NewLine+
				"assembly."+Environment.NewLine+
				""+Environment.NewLine+
				"A comment that follows the data structure format begins with the word"+Environment.NewLine+
				"\"struct\", followed by the name of the structure, followed by the data members"+Environment.NewLine+
				"that make up the structure, followed by the word \"endstruct\".  A sample structure"+Environment.NewLine+
				"comment is shown below."+Environment.NewLine+
				""+Environment.NewLine+
				"/*str"+"uct MyStructure"+Environment.NewLine+
				"  byte member1;"+Environment.NewLine+
				"  byte[2] member2;"+Environment.NewLine+
				"  byte member3;"+Environment.NewLine+
				"endstr"+"uct*/"+Environment.NewLine+
				""+Environment.NewLine+
				" Or in Visual Basic:"+Environment.NewLine+
				"'str"+"uct MyStructure"+Environment.NewLine+
				"'  byte member1;"+Environment.NewLine+
				"'  byte[2] member2;"+Environment.NewLine+
				"'  byte member3;"+Environment.NewLine+
				"'endstr"+"uct"+Environment.NewLine+
				""+Environment.NewLine+
				"Here, MyStructure is the name of the data structure.  Also note that each"+Environment.NewLine+
				"member (member1, member2, and member3) has three parts: the data type, the name"+Environment.NewLine+
				"of the member, and a semicolon."+Environment.NewLine+
				"If multiple consecutive members have the same data type, they can all"+Environment.NewLine+
				"be specified at once, using a comma to separate each member's name:"+Environment.NewLine+
				"byte member1, member2, member3;"+Environment.NewLine+
				"The types byte, sbyte, short, ushort, int, uint, long, ulong, float,"+Environment.NewLine+
				"and double have the same meaning as the respective type in the .NET Framework.  In addition, BYTE"+Environment.NewLine+
				"means byte, DWORD means uint, SHORT means short, word means ushort, dword means"+Environment.NewLine+
				"uint, LONG means int, and WORD means ushort.  Any other type is treated as a"+Environment.NewLine+
				"class that implements the IStructure interface."+Environment.NewLine+
				"By default, data types are written in little-endian byte order. By adding"+Environment.NewLine+
				"the suffix \"_be\"to a data type, the member will be written in big-endian byte"+Environment.NewLine+
				"order instead, for example, \"DWORD_be\"or \"long_be\"."+Environment.NewLine+
				"Finally, arrays of a data type are specified by adding a number enclosed"+Environment.NewLine+
				"in brackets to the corresponding member's name.  For example, the following"+Environment.NewLine+
				"specifies a member that represents an array of 16 bytes:"+Environment.NewLine+
				"byte member1[16];"+Environment.NewLine+
				"Instead of a data member, the following are supported as well:"+Environment.NewLine+
				"[noistructure];"+Environment.NewLine+
				"If the above appears, the structure will not include the IStructure interface"+Environment.NewLine+
				"and can't easily be read and written from a stream.  The advantage, though, is that the"+Environment.NewLine+
				"structure will not depend on the Peter O. Library, which includes BinaryIO and IStructure."+Environment.NewLine+
				"[immutable];"+Environment.NewLine+
				"If the above appears, the structure is immutable, that is, the structure's data"+Environment.NewLine+
				"can't be changed after it's created."+Environment.NewLine+
				"[orderby] member1, member2 descending, member3;"+Environment.NewLine+
				"If orderby appears, the structure includes the IComparable interface and"+Environment.NewLine+
				"and will be sorted in the order of the given members, separated by a comma.  The word"+Environment.NewLine+
				"\"descending\"can appear after a member's name to indicate that the structure is sorted"+Environment.NewLine+
				"in descending order of the member, rather than ascending order."+Environment.NewLine+
				"Finally, the word \"public\"can appear before the word \"struct\"to indicate that"+Environment.NewLine+
				"the structure is public rather than internal."+Environment.NewLine+"");
		}
		
		public static void Main(string[] args)
		{
			int count=0;
			try {
				foreach(string path in Directory.GetFiles(".","*.cs")){
					ParseStructures(path);
					count+=1;
				}
				foreach(string path in Directory.GetFiles(".","*.vb")){
					ParseStructures(path);
					count+=1;
				}
			} catch(Exception ex){
				Console.WriteLine(ex.GetType().FullName);
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
				Console.ReadLine();
			}
			if(count==0){
				Usage();
			}
		}
	}
}
