#include <QString>
#include <QtTest>

#include <constantbit.h>
#include <and.h>
#include <or.h>
#include <xor.h>
#include <not.h>

#include <intvalue.h>
#include <bytevalue.h>

#include <sha.h>

#define TABLE_SIZE 10
static unsigned int table[TABLE_SIZE] = { 368779801, 3340276706, 451503747, 453347691, 1827579633, 546540889, 3764776411, 324309599, 1449763902, 0xe3800000 };

uint RotRight(uint x, char n)
{
    return (x >> n) | (x << (32 - n));
}

void CalcRandomSHA(int size)
{
    QRandomGenerator rnd;

    QByteArray data(size, 0);
    for (int i=0; i<size; ++i)
        data[i] = static_cast<char>(rnd.generate());

    QByteArray native = QCryptographicHash::hash(data, QCryptographicHash::Sha256);
    SHA sha(data);

    QCOMPARE(native, sha.Result());
}

class TestTest : public QObject
{
    Q_OBJECT

private Q_SLOTS:
    void constants()
    {
        QVERIFY(ConstantBit(false).Get() == false);
        QVERIFY(ConstantBit(true).Get());

        for (size_t i = 0; i < TABLE_SIZE; ++i)
            QCOMPARE(IntValue(table[i]).Get(), table[i]);

        QCOMPARE(ByteValue(0x12).Get(), 0x12);
        QCOMPARE(ByteValue(0xFA).Get(), static_cast<char>(0xFA));
        QCOMPARE(ByteValue(0xfa4556).Get(), 0x56);

        ByteValue b0(0x3f);
        ByteValue b1(0x4d);
        ByteValue b2(0x8a);
        ByteValue b3(0x01);
        QCOMPARE(IntValue(b0, b1, b2, b3).Get(), 0x018a4d3f);

    }

    void basicBitOperations()
    {
        for (int v1 = 0; v1<=1; ++v1)
        {
            ConstantBit b1(v1);

            for (int v2 = 0; v2<=1; ++v2)
            {
                ConstantBit b2(v2);

                AND a(&b1, &b2);
                QVERIFY2((v1 && v2) == a.Get(), "AND fails");

                OR o(&b1, &b2);
                QVERIFY2((v1 || v2) == o.Get(), "OR fails");

                XOR x(&b1, &b2);
                QVERIFY2((v1 ^ v2) == x.Get(), "XOR fails");
            }

            NOT n(&b1);
            QVERIFY2(!v1 == n.Get(), "NOT fails");
        }
    }

    void testShift()
    {
        for (int i = 0; i < TABLE_SIZE; ++i)
        {
            uint a = table[i];

            for (char n = 0; n < 32; ++n)
            {
                QCOMPARE(IntValue(a).ShiftCirc(n).Get(), RotRight(a, n));
                QCOMPARE(IntValue(a).Shift(n).Get(), a >> n);
            }
        }
    }

    void testIntOperations()
    {
        for (int i = 1; i < TABLE_SIZE; ++i)
        {
            uint a = table[i - 1];
            uint b = table[i];

            IntValue ia(a);
            IntValue ib(b);

            QCOMPARE(ia.Not().Get(), ~a);
            QCOMPARE(ia.Xor(ib).Get(), a ^ b);
            QCOMPARE(ia.And(ib).Get(), a & b);
        }
    }

    void testAdd()
    {
        for (int i = 1; i < TABLE_SIZE; ++i)
        {
            uint a = table[i - 1];
            uint b = table[i];

            IntValue ib(b);
            QCOMPARE(IntValue(a).Add(ib).Get(), a + b);
        }
    }

    /*void testSHAParts()
    {
        for (int i = 0; i < TABLE_SIZE - 3; ++i)
        {
            uint x = table[i];
            uint y = table[i + 1];
            uint z = table[i + 2];

            IntValue X(x);
            IntValue Y(y);
            IntValue Z(z);

            QCOMPARE(Ch(X, Y, Z).Get(), (x & y) ^ (~x & z));
            QCOMPARE(EP0(X).Get(), RotRight(x, 2) ^ RotRight(x, 13) ^ RotRight(x, 22));
            QCOMPARE(EP1(X).Get(), RotRight(x, 6) ^ RotRight(x, 11) ^ RotRight(x, 25));
            QCOMPARE(Maj(X, Y, Z).Get(), (x & y) ^ (x & z) ^ (y & z));
            QCOMPARE(SIG0(X).Get(), RotRight(x, 7) ^ RotRight(x, 18) ^ (x >> 3));
            QCOMPARE(SIG1(X).Get(), RotRight(x, 17) ^ RotRight(x, 19) ^ (x >> 10));
        }
    }*/

    void testZeroSHA()
    {
        QByteArray bin = QByteArray::fromHex("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855");
        QCOMPARE(SHA(QByteArray()).Result(), bin);
    }

    void testRandomSHA()
    {
        CalcRandomSHA(0);
        CalcRandomSHA(1);
        CalcRandomSHA(63);
        CalcRandomSHA(64);
        CalcRandomSHA(65);
        CalcRandomSHA(812);
    }
};

QTEST_APPLESS_MAIN(TestTest)

#include "tst_testtest.moc"
