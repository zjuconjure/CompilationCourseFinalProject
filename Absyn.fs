(* File MicroC/Absyn.fs
   Abstract syntax of micro-C, an imperative language.
   sestoft@itu.dk 2009-09-25

   Must precede Interp.fs, Comp.fs and Contcomp.fs in Solution Explorer
 *)

module Absyn

type typ =
  | TypI                             (* Type int                    *)
  | TypC                             (* Type char                   *)
  | TypA of typ * int option         (* Array type                  *)
  | TypP of typ                      (* Pointer type                *)

and expr =                                                         
  | Access of access                 (* x    or  *p    or  a[e]     *)
  | Assign of access * expr          (* x=e  or  *p=e  or  a[e]=e   *)
  | Addr of access                   (* &x   or  &*p   or  &a[e]    *)
  | CstI of int                      (* Constant                    *)
  | Prim1 of string * expr           (* Unary primitive operator    *)
  | Prim2 of string * expr * expr    (* Binary primitive operator   *)
  | Andalso of expr * expr           (* Sequential and              *)
  | Orelse of expr * expr            (* Sequential or               *)
  | Call of string * expr list       (* Function call f(...)        *)
  | Doplus of access                 (* Double Plus                 *)
  | Dominus of access                (* Double Minus                *)
  | PreDoplus of access              (* Previous Double Plus        *)
  | PreDominus of access             (* Previous Double Minus       *)
  | PlusAssign of access * expr      (* Plus Assign                 *)
  | MinusAssign of access * expr     (* Minus Assign                *)
  | MultiAssign of access * expr     (* Multiply Assign             *)
  | DivideAssign of access * expr    (* Divide Assign               *)
  | Question of expr * expr * expr (* Condition expression          *)
  | Bitxor of expr * expr            (* bit operation XOR           *)
  | Bitand of expr * expr            (* bit operation AND           *)
  | Bitor of expr * expr             (* bit operation OR            *)
  | Bitleft of expr * expr           (* bit operation LEFT SHIFT    *)
  | Bitright of expr * expr          (* bit operation RIGHT SHIFT   *)
  | Bitnot of string * expr          (* bit operation NOT           *)
  | Max of expr * expr               (* Max function                *)
  | Min of expr * expr               (* Min function                *)
  | Abs of expr                      (* Abs function                *)
                                                                   
and access =                                                       
  | AccVar of string                 (* Variable access        x    *) 
  | AccDeref of expr                 (* Pointer dereferencing  *p   *)
  | AccIndex of access * expr        (* Array indexing         a[e] *)
                                                                   
and stmt =                                                         
  | If of expr * stmt * stmt         (* Conditional                 *)
  | While of expr * stmt             (* While loop                  *)
  | Expr of expr                     (* Expression statement   e;   *)
  | Return of expr option            (* Return from method          *)
  | Block of stmtordec list          (* Block: grouping and scope   *)
  | For of expr * expr * expr * stmt  (* For loop                   *)
  | Switch of expr * (int * stmt) list    (* Switch case no default *)
  | Switch2 of expr * (int * stmt) list * stmt (* Switch case       *)
                                                                   
and stmtordec =                                                    
  | Dec of typ * string              (* Local variable declaration  *)
  | Stmt of stmt                     (* A statement                 *)

and topdec = 
  | Fundec of typ option * string * (typ * string) list * stmt
  | Vardec of typ * string

and program = 
  | Prog of topdec list
