# Week 1

Target papers:
- [C. A. R. Hoare. An Axiomatic Basis for Computer Programming(1969)](https://www.cs.cmu.edu/~crary/819-f09/Hoare69.pdf)
- [Edsger W. Dijkstra. Guarded Commands, Nondeterminacy and Formal Derivation of Programs(1975)](https://www.cs.cmu.edu/~crary/819-f09/Dijkstra75.pdf)
- (Optional) [C. A. R. Hoare. Proof of a Program: FIND(1971)](https://www.cs.cmu.edu/~crary/819-f09/Hoare71.pdf)

---

Textbooks:

There are many classic textbooks on Hoare Logic and program verification. Here are a few recommendations which covers
the topics in the papers:
- [Software Foundations](https://softwarefoundations.cis.upenn.edu/) by Benjamin C. Pierce et al.
- [Verification of Sequential and Concurrent Programs](https://ir.cwi.nl/pub/14569/14569A.pdf) by Krzysztof R. Apt et al.
- [The Formal Semantics of Programming Languages: An Introduction](https://www.cin.ufpe.br/~if721/intranet/TheFormalSemanticsofProgrammingLanguages.pdf) by Glynn Winskel

---

Additional papers:

The Hoare 1969 paper is based on the Robert Floyd's 1967 paper:
- [Robert W. Floyd. Assigning Meanings to Programs(1967)](https://people.eecs.berkeley.edu/~necula/Papers/FloydMeaning.pdf)

Hoare logic has been extended to reason about concurrent programs:
- [Susan Owicki and David Gries. An Axiomatic Proof Technique for Parallel Programs(1976)](https://ecommons.cornell.edu/bitstream/handle/1813/6393/75-251.ps?sequence=2)

Here is a survey paper the impact of the GCL and Edsger Dijkstra's work on nondeterminism:

- [Krzysztof R. Apt and E.-R. Olderog. Nondeterminism and Guarded Commands](https://arxiv.org/pdf/2310.09004)

Here is a survey paper on the impact of Hoare logic on program verification:
- [Krzysztof R. Apt and E.-R. Olderog. Fifty Years of Hoare’s Logic](https://arxiv.org/pdf/1904.03917)

---

Practical resources:

- [Boogie](https://github.com/boogie-org/boogie) is an intermediate verification language, designed for building 
  program verifiers. Many program verifiers (e.g., Dafny, Viper, etc.) compile their input programs to Boogie and use the 
  Boogie toolchain to generate and discharge verification conditions. Boogie only supports sequential programs.
- [Civl](https://civl-verifier.github.io/) is a program verifier for concurrent programs. It extends the Boogie toolchain to support
  reasoning about concurrent programs using rely-guarantee reasoning.
- [Dafny](https://dafny.org/) is a verification-aware programming language with built-in specification constructs 
  (preconditions, postconditions, invariants) and an automatic program verifier.
- [Why3](https://www.why3.org/) is a platform for deductive program verification. It provides WhyML, a programming language with
  specification constructs, and an environment to generate verification conditions that can be discharged by various automated theorem provers.
- [KeY](https://key-project.org/) is a formal verification tool for Java programs. It uses dynamic logic to reason about 
  program correctness. KeY uses Java Modeling Language (JML) for writing specifications.
- [OpenJML](https://www.openjml.org/) is a tool for checking Java programs annotated with JML specifications. 
- [SPARK](https://learn.adacore.com/courses/intro-to-spark/chapters/01_Overview.html) is a programming language and toolset for high-assurance software development. 
  It includes a subset of Ada with formal specification constructs and a static analyzer to verify program correctness.
- [SPIN](https://spinroot.com/spin/whatispin.html) is a model checker for verifying the correctness of distributed software systems. 
  It uses Promela, a modeling language similar to Dijkstra's GCL, to describe system behavior.