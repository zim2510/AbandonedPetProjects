const msgerForm = document.getElementById("msger-inputarea");
const msgerInput = document.getElementById("msger-input");
const msgerChat = document.getElementById("msger-chat");

msgerForm.addEventListener("submit", async event => {
    event.preventDefault();

    const nickName = "Jim";
    const roomIdentifier = document.getElementById("roomIdentifier").value;
    const messageBody = msgerInput.value;
    const createdAt = new Date();
    const avatar =`https://ui-avatars.com/api/?name=${nickName}`;

    if (!messageBody) return;

    const resp = await fetch("/Chat/SendMessage", {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            "nickName": nickName,
            "roomIdentifier": roomIdentifier,
            "messageBody": messageBody,
            "createdAt": createdAt
        })
    });

    appendMessage(nickName, avatar, "right", messageBody, createdAt);
    msgerInput.value = "";

});

function appendMessage(name, img, side, text, date) {
    const msgHTML = `
    <div class="msg ${side}-msg">
      <div class="msg-img" style="background-image: url(${img})"></div>

      <div class="msg-bubble">
        <div class="msg-info">
          <div class="msg-info-name">${name}</div>
          <div class="msg-info-time">${formatDate(date)}</div>
        </div>

        <div class="msg-text">${text}</div>
      </div>
    </div>
  `;

    msgerChat.insertAdjacentHTML("beforeend", msgHTML);
    msgerChat.scrollTop += 500;
}


function formatDate(date) {
    const h = "0" + date.getHours();
    const m = "0" + date.getMinutes();

    return `${h.slice(-2)}:${m.slice(-2)}`;
}
