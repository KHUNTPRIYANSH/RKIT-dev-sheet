const productData = [
    {
        id: "1",
        name: "Keyboards",
        icon: "⌨️",
        expanded: true,
        hasItems: true, // Indicates it has sub-items (useful for dynamic loading)
        items: [
            {
                id: "1_1",
                name: "Mechanical Keyboard RGB",
                price: 120,
                icon: "⌨️",
                abcd: true,
            },
            {
                id: "1_2",
                name: "Membrane Keyboard Slim",
                price: 45,
                icon: "⌨️",

            },
            {
                id: "1_3",
                name: "Wireless Compact Keyboard",
                price: 75,
                icon: "⌨️",
                selected: true,
            },
        ],
    },
    {
        id: "2",
        name: "Mice",
        icon: "🖱️",
        abcd: true,
        hasItems: true,
        items: [
            {
                id: "2_1",
                name: "Wireless Gaming Mouse",
                price: 65,
                disabled: true,
                icon: "🖱️",
            },
            {
                id: "2_2",
                name: "Ergonomic Vertical Mouse",
                price: 55,
                icon: "🖱️",
            },
            {
                id: "2_3",
                name: "Trackball Mouse Pro",
                price: 80,
                icon: "🖱️",
            },
        ],
    },
    {
        id: "3",
        name: "Headsets",
        icon: "🎧",
        items: [
            {
                id: "3_1",
                name: "Noise-Canceling Headphones",
                price: 200,
                icon: "🎧",
            },
            {
                id: "3_2",
                name: "Wireless Gaming Headset",
                price: 150,
                icon: "🎧",
                disabled: true,
            },
        ],
    },
    {
        id: "4",
        name: "Webcams",
        icon: "📷",
        expanded: true,
        items: [
            {
                id: "4_1",
                name: "4K Webcam with Microphone",
                price: 120,

                icon: "📷",
            },
        ],
    },
    {
        id: "5",
        disabled: true,
        name: "Monitors",
        icon: "🖥️",
        items: [
            {
                id: "5_1",
                name: "32\" 4K Curved Monitor",
                price: 450,
                icon: "🖥️",
            },
        ],
    },
    {
        id: "6",
        name: "Speakers",
        icon: "🔊",
        items: [
            {
                id: "6_1",
                name: "Bluetooth Speaker",
                price: 100,
                icon: "🔊",
                disabled: true,
            },
            {
                id: "6_2",
                name: "Home Theater System",
                price: 500,
                icon: "🔊",
            },
        ],
    },
    {
        id: "7",
        name: "Microphones",
        icon: "🎙️",
        expanded: true,
        items: [
            {
                id: "7_1",
                name: "USB Podcast Microphone",
                price: 150,
                icon: "🎙️",
            },
        ],
    },
    {
        id: "8",
        name: "External Hard Drives",
        icon: "💽",
        items: [
            {
                id: "8_1",
                name: "1TB Portable HDD",
                price: 80,
                icon: "💽",
            },
        ],
    },
    {
        id: "9",
        name: "Printers",
        icon: "🖨️",
        items: [
            {
                id: "9_1",
                name: "All-in-One Printer",
                price: 300,
                icon: "🖨️",
            },
        ],
    },
    {
        id: "10",
        name: "Smart Watches",
        icon: "⌚",
        items: [
            {
                id: "10_1",
                name: "Fitness Tracker Smartwatch",
                price: 200,
                icon: "⌚",
            },
        ],
    },
    {
        id: "11",
        name: "VR Headsets",
        icon: "🕶️",
        items: [
            {
                id: "11_1",
                name: "Virtual Reality Headset",
                price: 400,
                icon: "🕶️",
            },
        ],
    },
    {
        id: "12",
        name: "USB Hubs",
        icon: "🔌",
        items: [
            {
                id: "12_1",
                name: "7-Port USB Hub",
                price: 50,
                icon: "🔌",
            },
        ],
    },
    {
        id: "13",
        name: "Gaming Chairs",
        icon: "🪑",
        items: [
            {
                id: "13_1",
                name: "Ergonomic Gaming Chair",
                price: 250,
                icon: "🪑",
            },
        ],
    },
    {
        id: "14",
        name: "Projectors",
        icon: "📽️",
        items: [
            {
                id: "14_1",
                name: "4K Home Projector",
                price: 600,
                icon: "📽️",
            },
        ],
    },
];
