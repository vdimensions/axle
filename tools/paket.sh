PAKET='../../../.paket/paket.exe'
if [ "$(uname)" == "Darwin" ]; then
    PAKET="mono $PAKET"
elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    PAKET="mono $PAKET"
elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW32_NT" ]; then
    # Do something under 32 bits Windows NT platform
elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    # Do something under 64 bits Windows NT platform
fi
if [ ! -f $PAKET ]; then
  eval "${PAKET%.exe}.bootstrapper.exe"
fi
$PAKET "$@"
