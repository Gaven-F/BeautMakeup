import type { Metadata } from "next";
import { ZCOOL_KuaiLe } from "next/font/google";
import "swiper/css";
import "./globals.css";

const font = ZCOOL_KuaiLe({ weight: "400", subsets: ["latin"] });

export const metadata: Metadata = {
	title: "Beaut Makeup Shop",
	description: "",
};

export default function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	return (
		<html lang="zh-cn">
			<body className={font.className}>{children}</body>
		</html>
	);
}
