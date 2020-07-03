using System;
using System.Collections.Generic;

namespace Kode {
    internal static class TokenHelper {
        private static readonly Dictionary<char, IToken> UniqueCharacterTokenMap = new Dictionary<char, IToken> {
            ['+'] = PositiveToken.Instance,
            ['-'] = NegativeToken.Instance,
            ['/'] = DivisionToken.Instance,
            ['('] = OpenParenthesesToken.Instance,
            [')'] = CloseParenthesesToken.Instance,
            ['%'] = ModulusToken.Instance
        };

        private static readonly Dictionary<char, TokenNode> ConflictingCharacterTokenMap = new Dictionary<char, TokenNode> {
            ['*'] = new TokenNode(new TokenNode('*', PowerToken.Instance), MultiplicationToken.Instance),
            ['p'] = new TokenNode(new TokenNode('o', new TokenNode('w', PowerToken.Instance)), null),
            ['<'] = new TokenNode(new TokenNode('<',  LeftBitshiftToken.Instance), null),
            ['>'] = new TokenNode(new TokenNode('>',  RightBitshiftToken.Instance), null)
        };

        public static int Find(ReadOnlyMemory<char> input, out IToken token) {
            if (UniqueCharacterTokenMap.TryGetValue(input.Span[0], out token)) {
                return 1;
            }

            if (ConflictingCharacterTokenMap.TryGetValue(input.Span[0], out TokenNode node)) {
                if (input.Length > 1) {
                    int length = 1;
                    token = UnwrapNode(input, node, ref length);
                    return length;
                }

                if (node.Token != null) {
                    token = node.Token;
                    return 1;
                }
            }

            return -1;
        }

        private static IToken UnwrapNode(ReadOnlyMemory<char> input, TokenNode node, ref int length) {
            while (input.Length > 1 && node.Next != null && input.Span[1] == node.Next.Key) {
                length++;
                node = node.Next;
                input = input.Slice(1);
            }

            return node.Token;
        }

        private class TokenNode {
            public char Key { get; }
            public TokenNode Next { get; }
            public IToken Token { get; }

            public TokenNode(char key, IToken token) {
                Key = key;
                Token = token;
            }
            
            public TokenNode(TokenNode next, IToken token) {
                Next = next;
                Token = token;
            }
            
            public TokenNode(char key, TokenNode next) {
                Key = key;
                Next = next;
            }
        }
    }
}