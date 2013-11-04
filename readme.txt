StructureBuilder.exe
Written by Peter O.

This program scans all C# and Visual Basic source files in the folder it's found in
for comments that define data structures.  It will generate the structure's code.
A data structure implements the IStructure interface and is easy to read and
write from data streams.  Each data structure is specified as internal to the
assembly.

A comment that follows the data structure format begins with the word
"struct", followed by the name of the structure, followed by the data members
that make up the structure, followed by the word "endstruct".  A sample structure
comment is shown below.

/*struct MyStructure
  byte member1;
  byte[2] member2;
  byte member3;
endstruct*/

 Or in Visual Basic:
'struct MyStructure
'  byte member1;
'  byte[2] member2;
'  byte member3;
'endstruct

Here, MyStructure is the name of the data structure.  Also note that each
member (member1, member2, and member3) has three parts: the data type, the name
of the member, and a semicolon.
If multiple consecutive members have the same data type, they can all
be specified at once, using a comma to separate each member's name:
byte member1, member2, member3;
The types byte, sbyte, short, ushort, int, uint, long, ulong, float,
and double have the same meaning as the respective type in the .NET Framework.  In addition, BYTE
means byte, DWORD means uint, SHORT means short, word means ushort, dword means
uint, LONG means int, and WORD means ushort.  Any other type is treated as a
class that implements the IStructure interface.
By default, data types are written in little-endian byte order. By adding
the suffix "_be"to a data type, the member will be written in big-endian byte
order instead, for example, "DWORD_be"or "long_be".
Finally, arrays of a data type are specified by adding a number enclosed
in brackets to the corresponding member's name.  For example, the following
specifies a member that represents an array of 16 bytes:

byte member1[16];

Instead of a data member, the following are supported as well:

[noistructure];

If the above appears, the structure will not include the IStructure interface
and can't easily be read and written from a stream.  The advantage, though, is that the
structure will not depend on the Peter O. Library, which includes BinaryIO and IStructure.

[immutable];

If the above appears, the structure is immutable, that is, the structure's data
can't be changed after it's created.

[orderby] member1, member2 descending, member3;

If orderby appears, the structure includes the IComparable interface and
and will be sorted in the order of the given members, separated by a comma.  The word
"descending"can appear after a member's name to indicate that the structure is sorted
in descending order of the member, rather than ascending order.

Finally, the word "public"can appear before the word "struct"to indicate that
the structure is public rather than internal.

