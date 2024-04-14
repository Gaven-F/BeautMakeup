// Code: PhoneLayout
type PhoneLayoutProps = {
	children: React.ReactNode;
	header: React.ReactNode;
	footer: React.ReactNode;
};

export default function PhoneLayout({
	children,
	header,
	footer,
}: PhoneLayoutProps) {
	return (
		<div className="flex flex-col w-full h-full">
			<div className="h-0 w-full basis-full bg-primary-100 bg-opacity-25 grid-container overflow-auto overflow-x-hidden">
				{header}
				{children}
			</div>
			<div className="h-16">{footer}</div>
		</div>
	);
}
