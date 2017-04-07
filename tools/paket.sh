_PATH="${0%/*}"
PAKET='${_PATH}/../.paket/paket.exe'
CALLER=''
if [ "$(uname)" == "Darwin" ]; then
    CALLER="mono ";
elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    CALLER="mono ";
#elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW32_NT" ]; then
    # Do something under 32 bits Windows NT platform
#elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    # Do something under 64 bits Windows NT platform
fi
if [ ! -f $PAKET ]; then
  eval "$CALLER${PAKET%.exe}.bootstrapper.exe"
fi
eval "$CALLER$PAKET \"$@\""
