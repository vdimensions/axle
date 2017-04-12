PAKET='${0%/*}/../.paket/paket.exe'
CALLEXE=''
if [ "$(uname)" == "Darwin" ]; then
  CALLEXE="mono ";
elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
  CALLEXE="mono ";
#elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW32_NT" ]; then
  # Do something under 32 bits Windows NT platform
#elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
  # Do something under 64 bits Windows NT platform
fi
if [ ! -f $PAKET ]; then
  eval "$CALLEXE${PAKET%.exe}.bootstrapper.exe"
fi
eval "$CALLEXE$PAKET \"$@\""
