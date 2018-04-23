#ifndef SHA_H
#define SHA_H

#include "block256.h"
#include "intvalue.h"

class SHA
{
public:
    SHA(QByteArray input);

    QByteArray Result() const;

    QList<BitValue*> OutBits() const;

private:
    Block256 state;
};

#endif // SHA_H
