const { makeWASocket, DisconnectReason, useMultiFileAuthState } = require('@adiwajshing/baileys');
const qrcode = require('qrcode-terminal');

(async () => {
    const { state, saveCreds } = await useMultiFileAuthState('auth_info');
    const sock = makeWASocket({
        auth: state,
        printQRInTerminal: true, // QR-код будет печататься в консоли
    });

    // Событие: подключение
    sock.ev.on('connection.update', (update) => {
        const { connection, lastDisconnect, qr } = update;

        if (qr) {
            // Если нужно отобразить QR-код в терминале
            qrcode.generate(qr, { small: true });
        }

        if (connection === 'close') {
            const shouldReconnect =
                (lastDisconnect.error)?.output?.statusCode !== DisconnectReason.loggedOut;
            console.log('Соединение закрыто, попытка переподключения:', shouldReconnect);

            if (shouldReconnect) {
                main();
            }
        } else if (connection === 'open') {
            console.log('Успешно подключено к WhatsApp');
        }
    });

    // Событие: получение сообщений
    sock.ev.on('messages.upsert', async (msgUpdate) => {
        const messages = msgUpdate.messages;
        if (!messages || !messages[0].message) return;

        const message = messages[0];
        const sender = message.key.remoteJid;
        const text = message.message.conversation || message.message.extendedTextMessage?.text;

        console.log(`Получено сообщение от ${sender}: ${text}`);

        // Ответ на полученное сообщение
        if (text === 'Привет') {
            await sock.sendMessage(sender, { text: 'Привет! Чем могу помочь?' });
        } else {
            await sock.sendMessage(sender, { text: `Вы написали: ${text}` });
        }
    });

    // Сохраняем состояние при изменении
    sock.ev.on('creds.update', saveCreds);
})();