using System;
using System.Collections.Generic;
using System.Linq;

namespace WhileVerifier.Core.AbstractSyntaxTree
{

    public abstract class Node
    {
        public SourcePosition? Position { get; set; }

        public abstract void Accept(IASTVisitor visitor);

        public abstract T Accept<T>(IASTVisitor<T> visitor);
    }

    public record SourcePosition(int Line, int Column, int StartIndex, int Length);

    public interface IASTVisitor
    {
        void VisitProgram(Program program);
        void VisitAssignmentStatement(AssignmentStatement assignment);
        void VisitSequenceStatement(SequenceStatement sequence);
        void VisitIfStatement(IfStatement ifStatement);
        void VisitWhileStatement(WhileStatement whileStatement);
        void VisitSkipStatement(SkipStatement skipStatement);

        void VisitVariableExpression(VariableExpression variable);
        void VisitNumberExpression(NumberExpression number);
        void VisitBinaryExpression(BinaryExpression binary);
        void VisitUnaryExpression(UnaryExpression unary);

        void VisitAssertion(Assertion assertion);
        void VisitBinaryAssertion(BinaryAssertion binaryAssertion);
        void VisitUnaryAssertion(UnaryAssertion unaryAssertion);
        void VisitTrueAssertion(TrueAssertion trueAssertion);
        void VisitFalseAssertion(FalseAssertion falseAssertion);
    }

    public interface IASTVisitor<T>
    {
        T VisitProgram(Program program);
        T VisitAssignmentStatement(AssignmentStatement assignment);
        T VisitSequenceStatement(SequenceStatement sequence);
        T VisitIfStatement(IfStatement ifStatement);
        T VisitWhileStatement(WhileStatement whileStatement);
        T VisitSkipStatement(SkipStatement skipStatement);

        T VisitVariableExpression(VariableExpression variable);
        T VisitNumberExpression(NumberExpression number);
        T VisitBinaryExpression(BinaryExpression binary);
        T VisitUnaryExpression(UnaryExpression unary);

        T VisitAssertion(Assertion assertion);
        T VisitBinaryAssertion(BinaryAssertion binaryAssertion);
        T VisitUnaryAssertion(UnaryAssertion unaryAssertion);
        T VisitTrueAssertion(TrueAssertion trueAssertion);
        T VisitFalseAssertion(FalseAssertion falseAssertion);
    }

    public class Program : Node
    {
        public Assertion Precondition { get; set; } = null!;
        public Statement Statement { get; set; } = null!;
        public Assertion Postcondition { get; set; } = null!;

        public override void Accept(IASTVisitor visitor) => visitor.VisitProgram(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitProgram(this);

        public override string ToString()
            => $"{{{Precondition}}} {Statement} {{{Postcondition}}}";
    }


    public abstract class Statement : Node { }

    public class AssignmentStatement : Statement
    {
        public string VariableName { get; set; } = string.Empty;
        public Expression Expression { get; set; } = null!;

        public override void Accept(IASTVisitor visitor) => visitor.VisitAssignmentStatement(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitAssignmentStatement(this);

        public override string ToString() => $"{VariableName} := {Expression}";
    }

    public class SequenceStatement : Statement
    {
        public List<Statement> Statements { get; set; } = new();

        public override void Accept(IASTVisitor visitor) => visitor.VisitSequenceStatement(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitSequenceStatement(this);

        public override string ToString() => string.Join("; ", Statements);
    }

    public class IfStatement : Statement
    {
        public Expression Condition { get; set; } = null!;
        public Statement ThenStatement { get; set; } = null!;
        public Statement? ElseStatement { get; set; }

        public override void Accept(IASTVisitor visitor) => visitor.VisitIfStatement(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitIfStatement(this);

        public override string ToString()
        {
            var result = $"if {Condition} then {ThenStatement}";
            if (ElseStatement != null)
                result += $" else {ElseStatement}";
            return result;
        }
    }

    public class WhileStatement : Statement
    {
        public Expression Condition { get; set; } = null!;
        public Statement Body { get; set; } = null!;

        public Assertion? Invariant { get; set; }

        public override void Accept(IASTVisitor visitor) => visitor.VisitWhileStatement(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitWhileStatement(this);

        public override string ToString()
        {
            var result = $"while {Condition} do {Body}";
            if (Invariant != null)
                result += $" [invariant: {Invariant}]";
            return result;
        }
    }


    public class SkipStatement : Statement
    {
        public override void Accept(IASTVisitor visitor) => visitor.VisitSkipStatement(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitSkipStatement(this);

        public override string ToString() => "skip";
    }

 
    public abstract class Expression : ASTNode { }

    public class VariableExpression : Expression
    {
        public string Name { get; set; } = string.Empty;

        public override void Accept(IASTVisitor visitor) => visitor.VisitVariableExpression(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitVariableExpression(this);

        public override string ToString() => Name;
    }


    public class NumberExpression : Expression
    {
        public int Value { get; set; }

        public override void Accept(IASTVisitor visitor) => visitor.VisitNumberExpression(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitNumberExpression(this);

        public override string ToString() => Value.ToString();
    }


    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; } = null!;
        public BinaryOperator Operator { get; set; }
        public Expression Right { get; set; } = null!;

        public override void Accept(IASTVisitor visitor) => visitor.VisitBinaryExpression(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitBinaryExpression(this);

        public override string ToString() => $"({Left} {Operator.ToSymbol()} {Right})";
    }

    public class UnaryExpression : Expression
    {
        public UnaryOperator Operator { get; set; }
        public Expression Operand { get; set; } = null!;

        public override void Accept(IASTVisitor visitor) => visitor.VisitUnaryExpression(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitUnaryExpression(this);

        public override string ToString() => $"({Operator.ToSymbol()}{Operand})";
    }


    public enum BinaryOperator
    {
        Add,         
        Subtract,    
        Multiply,    
        Divide,      
        Modulo,     
        Equal,       
        NotEqual,    
        LessThan,    
        LessEqual,  
        GreaterThan, 
        GreaterEqual,

        And,  
        Or,    
        Implies      
    }

    public enum UnaryOperator
    {
        Minus,    
        Not          
    }


    public abstract class Assertion : ASTNode
    {
       
        public abstract Assertion Substitute(string variable, Expression expression);
    }

    public class BinaryAssertion : Assertion
    {
        public Expression Left { get; set; } = null!;
        public BinaryOperator Operator { get; set; }
        public Expression Right { get; set; } = null!;

        public override void Accept(IASTVisitor visitor) => visitor.VisitBinaryAssertion(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitBinaryAssertion(this);

        public override Assertion Substitute(string variable, Expression expression)
        {
            return new BinaryAssertion
            {
                Left = SubstituteInExpression(Left, variable, expression),
                Operator = Operator,
                Right = SubstituteInExpression(Right, variable, expression),
                Position = Position
            };
        }

        private Expression SubstituteInExpression(Expression expr, string variable, Expression replacement)
        {
            return expr switch
            {
                VariableExpression v when v.Name == variable => replacement,
                BinaryExpression b => new BinaryExpression
                {
                    Left = SubstituteInExpression(b.Left, variable, replacement),
                    Operator = b.Operator,
                    Right = SubstituteInExpression(b.Right, variable, replacement),
                    Position = b.Position
                },
                UnaryExpression u => new UnaryExpression
                {
                    Operator = u.Operator,
                    Operand = SubstituteInExpression(u.Operand, variable, replacement),
                    Position = u.Position
                },
                _ => expr
            };
        }

        public override string ToString() => $"({Left} {Operator.ToSymbol()} {Right})";
    }

    public class UnaryAssertion : Assertion
    {
        public UnaryOperator Operator { get; set; }
        public Assertion Operand { get; set; } = null!;

        public override void Accept(IASTVisitor visitor) => visitor.VisitUnaryAssertion(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitUnaryAssertion(this);

        public override Assertion Substitute(string variable, Expression expression)
        {
            return new UnaryAssertion
            {
                Operator = Operator,
                Operand = Operand.Substitute(variable, expression),
                Position = Position
            };
        }

        public override string ToString() => $"({Operator.ToSymbol()}{Operand})";
    }

    public class TrueAssertion : Assertion
    {
        public override void Accept(IASTVisitor visitor) => visitor.VisitTrueAssertion(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitTrueAssertion(this);

        public override Assertion Substitute(string variable, Expression expression) => this;
        public override string ToString() => "true";
    }

    public class FalseAssertion : Assertion
    {
        public override void Accept(IASTVisitor visitor) => visitor.VisitFalseAssertion(this);
        public override T Accept<T>(IASTVisitor<T> visitor) => visitor.VisitFalseAssertion(this);

        public override Assertion Substitute(string variable, Expression expression) => this;
        public override string ToString() => "false";
    }

    public static class OperatorExtensions
    {
        public static string ToSymbol(this BinaryOperator op) => op switch
        {
            BinaryOperator.Add => "+",
            BinaryOperator.Subtract => "-",
            BinaryOperator.Multiply => "*",
            BinaryOperator.Divide => "/",
            BinaryOperator.Modulo => "%",
            BinaryOperator.Equal => "=",
            BinaryOperator.NotEqual => "!=",
            BinaryOperator.LessThan => "<",
            BinaryOperator.LessEqual => "<=",
            BinaryOperator.GreaterThan => ">",
            BinaryOperator.GreaterEqual => ">=",
            BinaryOperator.And => "∧",
            BinaryOperator.Or => "∨",
            BinaryOperator.Implies => "→",
            _ => "?"
        };

        public static string ToSymbol(this UnaryOperator op) => op switch
        {
            UnaryOperator.Minus => "-",
            UnaryOperator.Not => "¬",
            _ => "?"
        };

        public static int GetPrecedence(this BinaryOperator op) => op switch
        {
            BinaryOperator.Or => 1,
            BinaryOperator.And => 2,
            BinaryOperator.Implies => 3,
            BinaryOperator.Equal => 4,
            BinaryOperator.NotEqual => 4,
            BinaryOperator.LessThan => 4,
            BinaryOperator.LessEqual => 4,
            BinaryOperator.GreaterThan => 4,
            BinaryOperator.GreaterEqual => 4,
            BinaryOperator.Add => 5,
            BinaryOperator.Subtract => 5,
            BinaryOperator.Multiply => 6,
            BinaryOperator.Divide => 6,
            BinaryOperator.Modulo => 6,
            _ => 0
        };

        public static bool IsLeftAssociative(this BinaryOperator op) => op switch
        {
            BinaryOperator.Implies => false, 
            _ => true 
        };
    }

    public static class AST
    {
        public static Program Program(Assertion pre, Statement stmt, Assertion post)
            => new() { Precondition = pre, Statement = stmt, Postcondition = post };

        // Statements
        public static AssignmentStatement Assignment(string variable, Expression expression)
            => new() { VariableName = variable, Expression = expression };

        public static SequenceStatement Sequence(params Statement[] statements)
            => new() { Statements = statements.ToList() };

        public static IfStatement If(Expression condition, Statement thenStmt, Statement? elseStmt = null)
            => new() { Condition = condition, ThenStatement = thenStmt, ElseStatement = elseStmt };

        public static WhileStatement While(Expression condition, Statement body, Assertion? invariant = null)
            => new() { Condition = condition, Body = body, Invariant = invariant };

        public static SkipStatement Skip() => new();

        // Expressions
        public static VariableExpression Variable(string name) => new() { Name = name };
        public static NumberExpression Number(int value) => new() { Value = value };

        public static BinaryExpression Binary(Expression left, BinaryOperator op, Expression right)
            => new() { Left = left, Operator = op, Right = right };

        public static UnaryExpression Unary(UnaryOperator op, Expression operand)
            => new() { Operator = op, Operand = operand };

        public static BinaryExpression Add(Expression left, Expression right)
            => Binary(left, BinaryOperator.Add, right);
        public static BinaryExpression Subtract(Expression left, Expression right)
            => Binary(left, BinaryOperator.Subtract, right);
        public static BinaryExpression Multiply(Expression left, Expression right)
            => Binary(left, BinaryOperator.Multiply, right);
        public static BinaryExpression Equal(Expression left, Expression right)
            => Binary(left, BinaryOperator.Equal, right);
        public static BinaryExpression LessThan(Expression left, Expression right)
            => Binary(left, BinaryOperator.LessThan, right);
        public static BinaryExpression GreaterThan(Expression left, Expression right)
            => Binary(left, BinaryOperator.GreaterThan, right);

        // Assertions
        public static TrueAssertion True() => new();
        public static FalseAssertion False() => new();

        public static BinaryAssertion Assert(Expression left, BinaryOperator op, Expression right)
            => new() { Left = left, Operator = op, Right = right };

        public static UnaryAssertion Assert(UnaryOperator op, Assertion operand)
            => new() { Operator = op, Operand = operand };
    }
}